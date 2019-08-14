using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BLL.Models;
using VacationTrackingSoftware.ViewModels;
using Microsoft.AspNetCore.Authorization;
using VacationTrackingSoftware.Helpers;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ManagerController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private IManagerService _managerService;
        private IWorkerService _workerService;
        private IWorkerRepository _workerRepository;
        public ManagerController(
            UserManager<AppUser> userManager,
           IManagerService managerService,
            IWorkerService workerService,
            IWorkerRepository workerRepository)
        {
            _userManager = userManager;       
            _managerService = managerService;
            _workerService = workerService;
            _workerRepository = workerRepository;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]RegistrationManagerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates", ModelState));
            }
            try {
                //var response = _workerService.SendDataToWorker(model.Email, model.FirstName + model.LastName, model.Password);
                //if (response.Successful == false) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration", response.Errors.FirstOrDefault(), ModelState));
                AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName };
                var result = await _userManager.CreateAsync(userIdentity, model.Password);
                if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                else
                {
                    Worker Worker = new Worker() { DateRecruitment = DateTime.Now, User = userIdentity };
                    _workerRepository.Create(Worker);
                    await _userManager.AddToRoleAsync(userIdentity, model.Role);
                    _managerService.UpdateTeams(userIdentity, model.TeamsId);
                    return new OkObjectResult("Account created");
                }
            }
            catch { return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates", ModelState)); }           
        }
    }
}