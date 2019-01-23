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

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private readonly UserManager<AppUser> _userManager;
        private IWorkerRepository _workerRepository;
        public ManagerController(IEmployeeService employeeService, IUserVacationRequestRepository userVacationRequestRepository, UserManager<AppUser> userManager, IWorkerRepository workerRepository)
        {
            _employeeService = employeeService;
            _userVacationRequestRepository = userVacationRequestRepository;
            _userManager = userManager;
            _workerRepository = workerRepository;
        }
        [HttpPost("[action]")]
        public UserVacationRequest ChangeStatus([FromBody] ChangeStatusViewModel changeStatusViewModel) {
            var uservacationRequest=_userVacationRequestRepository.GetById(changeStatusViewModel.Id);
            //choose == true ? uservacationRequest.Status = (int)StatusesRequest.Accepted : uservacationRequest.Status= (int)StatusesRequest.Declined;
            if (changeStatusViewModel.Choose == true) uservacationRequest.Status = (int)StatusesRequest.Accepted;
            if (changeStatusViewModel.Choose == false) uservacationRequest.Status = (int)StatusesRequest.Declined;
            _userVacationRequestRepository.Update(uservacationRequest);
            _userVacationRequestRepository.Save();
            return uservacationRequest;
        }
    }
}