using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
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
        private readonly IMapper _mapper;
        private IAccountService _accountService;
        private IWorkerRepository _workerRepository;
        private ProjectContext _appDbContext;

        public AccountController(
            UserManager<AppUser> userManager, 
            IMapper mapper,
            IAccountService accountService,
            IWorkerRepository workerRepository,
            ProjectContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _accountService = accountService;
            _workerRepository = workerRepository;
            _appDbContext = appDbContext; }

        //POST api/accounts
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCreateEmployee([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates. Please try again", ModelState));
            }

            AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName};
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration",result.Errors.First().Description, ModelState));
            else {
                await _userManager.AddToRoleAsync(userIdentity, model.Role);
                _accountService.CreateWorkerAndTeamUser(userIdentity, model.TeamId);
                //SendDataToWorker(userIdentity.Email, userIdentity.UserName, model.Password);
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

            Worker worker = new Worker { DateRecruitment = DateTime.Now };
            AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName };
            _workerRepository.Save();
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            else
            {
                await _userManager.AddToRoleAsync(userIdentity, model.Role);
                _accountService.CreateWorkerAndUpdateTeams(userIdentity, model.TeamsId);
                //SendDataToWorker(userIdentity.Email, userIdentity.UserName, model.Password);
                return new OkObjectResult("Account created");
            }
        }
        private void SendDataToWorker(string email, string nickName, string password) {
            var fromAddress = new MailAddress("sashapopov24051996@gmail.com", "Company");
            var toAddress = new MailAddress(email, "Worker");
            string fromPassword = "Control1996";
            string subject = "Login and password";
            string body = "Your nickName:"+nickName+", and your password:"+password;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        [HttpGet("[action]")]
        public List<Team> GetAllTeams() {
            return _accountService.GetAllTeams();
        }

    }
}