using System.Linq;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.Helpers;
using VacationTrackingSoftware.ViewModels;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IWorkerService _workerService;

        public WorkerController(
            UserManager<AppUser> userManager,
            IWorkerService workerService,
            IWorkerRepository workerRepository)
        {
            _userManager = userManager;
            _workerService = workerService;
        }

        //POST api/accounts
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates. Please try again.", ModelState));
            }
            try
            {
                var response = _workerService.SendDataToWorker(model.Email, model.FirstName + model.LastName, model.Password);
                if (response.Successful == false) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration", response.Errors.FirstOrDefault(), ModelState));

                AppUser userIdentity = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.FirstName + model.LastName };
                var result = await _userManager.CreateAsync(userIdentity, model.Password);
                if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorToModelState("registration", result.Errors.First().Description, ModelState));
                else
                {
                    await _userManager.AddToRoleAsync(userIdentity, model.Role);
                    _workerService.CreateWorkerAndTeamUser(userIdentity, model.TeamId);
                    return new OkObjectResult("Account created");
                }
            }
            catch {
                return BadRequest(Errors.AddErrorToModelState("registration", "Invalid dates. Please try later.", ModelState));
            }          
        }
    }
}