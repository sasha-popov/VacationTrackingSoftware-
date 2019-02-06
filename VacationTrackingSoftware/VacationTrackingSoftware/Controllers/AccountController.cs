using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
using BLL.Services;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.Helpers;
using VacationTrackingSoftware.ViewModels;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IAccountService _accountService;
        private IWorkerRepository _workerRepository;

        public AccountController(
            UserManager<AppUser> userManager,
            IAccountService accountService,
            IWorkerRepository workerRepository)
        {
            _userManager = userManager;
            _accountService = accountService;
            _workerRepository = workerRepository;
        }

        //POST api/accounts
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCreateEmployee([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates. Please try again", ModelState));
            }
            var response = SendDataToWorker(model.Email, model.FirstName + model.LastName, model.Password);
            if (response.Result == false) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration", response.Errors.FirstOrDefault(), ModelState));

            AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName };
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration", result.Errors.First().Description, ModelState));
            else
            {
                await _userManager.AddToRoleAsync(userIdentity, model.Role);
                _accountService.CreateWorkerAndTeamUser(userIdentity, model.TeamId);
                return new OkObjectResult("Account created");
            }

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCreateManager([FromBody]RegistrationManagerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates", ModelState));
            }
            var response = SendDataToWorker(model.Email, model.FirstName + model.LastName, model.Password);
            if (response.Result == false) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration", response.Errors.FirstOrDefault(), ModelState));
            Worker worker = new Worker { DateRecruitment = DateTime.Now };
            AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName };
            _workerRepository.Save();
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            else
            {
                await _userManager.AddToRoleAsync(userIdentity, model.Role);
                _accountService.CreateWorkerAndUpdateTeams(userIdentity, model.TeamsId);
                SendDataToWorker(userIdentity.Email, userIdentity.UserName, model.Password);
                return new OkObjectResult("Account created");
            }
        }
        private ResponseForRequest SendDataToWorker(string email, string nickName, string password)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("tansked2000@gmail.com", "control1996")
                };

                using (var message = new MailMessage("tansked2000@gmail.com", email)
                {
                    Subject = "Login and password",
                    Body = "Your nickName:" + nickName + ", and your password:" + password
                })
                {
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                return new ResponseForRequest() { Result = false, Errors = new List<string>() { "Incorrecr email. Please try again!" } };
            }

            return new ResponseForRequest() { Result = true };

        }
        [HttpGet("[action]")]
        public List<Team> GetAllTeams()
        {
            return _accountService.GetAllTeams();
        }

        [HttpPost("[action]")]
        public ResponseForRequest UpdateUserTeam(UpdateUserTeam updateUserTeam)
        {
            AppUser userForUpdate = _userManager.FindByIdAsync(updateUserTeam.UserId).Result;
            var role = _userManager.GetRolesAsync(userForUpdate).Result.FirstOrDefault();
            return _accountService.UpdateUserTeam(userForUpdate, role, updateUserTeam.TeamId, updateUserTeam.TeamsId);
        }
    }
}