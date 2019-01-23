using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.Helpers;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private IAccountService _accountService;
        private IWorkerRepository _workerRepository;
        private ProjectContext _appDbContext;
        private ITeamRepository _teamRepository;

        public AccountController(
            UserManager<AppUser> userManager, 
            IMapper mapper,
            IAccountService accountService,
            IWorkerRepository workerRepository,
            ProjectContext appDbContext,
            ITeamRepository teamRepository
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _accountService = accountService;
            _workerRepository = workerRepository;
            _appDbContext = appDbContext;
            _teamRepository = teamRepository;
        }

        //POST api/accounts
       [HttpPost("[action]")]
        public async Task<IActionResult> PostCreate([FromBody]RegistrationViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            
            Worker worker = new Worker { DateRecruitment = DateTime.Now };
            AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName};
            _workerRepository.Save();
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            else {
                await _userManager.AddToRoleAsync(userIdentity, model.Role);
            }
            _accountService.CreateWorkerAndTeamUser(userIdentity, model.TeamId, model.Role);
            //SendDataToWorker(userIdentity.Email, userIdentity.UserName, model.Password);
            return new OkObjectResult("Account created");
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
            return _teamRepository.GetAll().ToList();
        }

    }
    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public int TeamId { get; set; }
    }
}