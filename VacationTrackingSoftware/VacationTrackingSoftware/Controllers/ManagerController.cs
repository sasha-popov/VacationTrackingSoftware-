using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BLL.Statuses;
using BLL.Models;
using VacationTrackingSoftware.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize]
    public class ManagerController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private readonly UserManager<AppUser> _userManager;
        private IWorkerRepository _workerRepository;
        private ITeamRepository _teamRepository;
        private ITeamUserRepository _teamUserRepository;
        public ManagerController(IEmployeeService employeeService, IUserVacationRequestRepository userVacationRequestRepository,
            UserManager<AppUser> userManager, IWorkerRepository workerRepository, ITeamRepository teamRepository, ITeamUserRepository teamUserRepository)
        {
            _employeeService = employeeService;
            _userVacationRequestRepository = userVacationRequestRepository;
            _userManager = userManager;
            _workerRepository = workerRepository;
            _teamRepository = teamRepository;
            _teamUserRepository = teamUserRepository;
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "Manager")]
        public UserVacationRequest ChangeStatus([FromBody] ChangeStatusViewModel changeStatusViewModel)
        {
            var uservacationRequest = _userVacationRequestRepository.GetById(changeStatusViewModel.Id);
            if (changeStatusViewModel.Choose == true) uservacationRequest.Status = (int)StatusesRequest.Accepted;
            if (changeStatusViewModel.Choose == false) uservacationRequest.Status = (int)StatusesRequest.Declined;
            _userVacationRequestRepository.Update(uservacationRequest);
            _userVacationRequestRepository.Save();
            return uservacationRequest;
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Manager")]
        public List<Team> GetTeamsForManager()
        {
            var userId = User.FindFirst("id").Value;

            var result = _teamRepository.FindTeamsByManager(userId);
            return result;
        }
    }
}