using System;
using System.Collections.Generic;
using System.Linq;
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

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
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
            ProjectContext appDbContext
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _accountService = accountService;
            _workerRepository = workerRepository;
            _appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCreate(RegistrationViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var userIdentity = _mapper.Map<AppUser>(model);

            var result =await _userManager.CreateAsync(userIdentity, "password");

            //if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.AddAsync(new Worker {Name= userIdentity.FirstName});
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }


    }
    public class RegistrationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
    }
}