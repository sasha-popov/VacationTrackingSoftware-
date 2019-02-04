using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Services;
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
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private readonly UserManager<AppUser> _userManager;
        private IWorkerRepository _workerRepository;
        private ITeamUserRepository _teamUserRepository;
        private ITeamRepository _teamRepository;
        public EmployeeController(IEmployeeService employeeService, IUserVacationRequestRepository userVacationRequestRepository, UserManager<AppUser> userManager, IWorkerRepository workerRepository,
            ITeamUserRepository teamUserRepository,
            ITeamRepository teamRepository
            )
        {
            _employeeService = employeeService;
            _userVacationRequestRepository = userVacationRequestRepository;
            _userManager = userManager;
            _workerRepository = workerRepository;
            _teamUserRepository = teamUserRepository;
            _teamRepository = teamRepository;
        }

        [HttpPost("[action]")]
        public IActionResult CreateVacationRequest(UserVacationRequestDTO newVacationRequest)
        {
            if (!ModelState.IsValid || newVacationRequest.StartDate<DateTime.Now)
            {
                return BadRequest(Errors.AddErrorToModelState("vacationRequestError", "Invalid data, please try again", ModelState));
            }
            var vacationRequest= _employeeService.CreateVacationRequest(newVacationRequest);
            if (vacationRequest != null)
            {
                //string count=vacationRequest.Payment.ToString();
                return new OkObjectResult(vacationRequest);
            }
                return BadRequest(Errors.AddErrorToModelState("vacationRequestError", "You do not have so many vacation days, or invalid DateTime.Please check the data and try again", ModelState));
        }

        [HttpGet("[action]")]
        public List<UserVacationRequestDTO> ShowUserVacationRequest()
        {
            var userId = User.FindFirst("id").Value;
            AppUser user = _userManager.FindByIdAsync(userId).Result;
            var check = _userManager.IsInRoleAsync(user, "Employee");
            if (check.Result)
            {
                var result = _employeeService.ShowUserVacationRequest(userId);
                return result;
            }
            return null;
        }

        [HttpGet("[action]")]
        //[Authorize(Roles="Manager")]
        public List<UserVacationRequestDTO> ShowUserVacationRequestForManager()
        {
            var userId = User.FindFirst("id").Value;
            AppUser user = _userManager.FindByIdAsync(userId).Result;
            var check = _userManager.IsInRoleAsync(user, "Manager");
            if (check.Result)
            {
                var result = _employeeService.ShowUserVacationRequestForManager(user);
                return result;
            }

            return null;
        }

        [HttpDelete("[action]/{startDate}/{endDate}")]
        public void deleteUserVacationRequest(DateTime startDate, DateTime endDate)
        {
            //change this
            var userId = User.FindFirst("id").Value;
            var currentVacationRequest = _userVacationRequestRepository.GetWithWorker(startDate, endDate, userId);
            if (currentVacationRequest != null && startDate>DateTime.Now)
            {
                _userVacationRequestRepository.Delete(currentVacationRequest);
                _userVacationRequestRepository.Save();
            }

        }
        [HttpGet("[action]")]
        public List<WorkersViewModel> GetALLWorkersForHrUser()
        {
            var allEmployee = _teamUserRepository.GetAllWithDetails();
            List<WorkersViewModel> workersViewModel = new List<WorkersViewModel>();
            allEmployee.ForEach(x =>
            {
                if (x.Team != null) x.Team.TeamUsers = null;
                workersViewModel.Add(new WorkersViewModel() { FirstName = x.User.FirstName, LastName = x.User.LastName, Id = x.User.Id, Team = x.Team, Role = (int)Roles.Employee });
            });
            
            List<AppUser> allManagers = _userManager.GetUsersInRoleAsync("Manager").Result.ToList();
            allManagers.ForEach(manager =>
            {
                workersViewModel.Add(new WorkersViewModel() { FirstName = manager.FirstName, Id = manager.Id, LastName = manager.LastName, Role = (int)Roles.Manager, Teams = new List<Team>() });
            });

            List<Team> teams = _teamRepository.AllTeamsWithManager().ToList();
            teams.ForEach(team =>
            {
                if (team.Manager != null) {
                    var result = workersViewModel.FirstOrDefault(x => x.Id == team.Manager.Id);
                    result.Teams.Add(team);
                }
            });

            return workersViewModel;
        }


    }


}