using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
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
    public class VacationRequestController : ControllerBase
    {
        private IVacationRequestService _vacationRequestService;
        private readonly UserManager<AppUser> _userManager;
        public VacationRequestController(
            IVacationRequestService employeeService,           
            UserManager<AppUser> userManager)
        {
            _vacationRequestService = employeeService;
            _userManager = userManager;            
        }
        [HttpPut("[action]")]
        [Authorize(Roles = "Manager")]
        public UserVacationRequest ChangeStatus([FromBody] ChangeStatusViewModel changeStatusViewModel)
        {
            return _vacationRequestService.ChangeStatus(changeStatusViewModel.Id, changeStatusViewModel.Choose);
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Employee")]
        public List<UserVacationRequestDTO> ShowUserVacationRequest()
        {
            var userId = User.FindFirst("id").Value;
            AppUser user = _userManager.FindByIdAsync(userId).Result;
            var check = _userManager.IsInRoleAsync(user, "Employee");
            if (check.Result)
            {
                var result = _vacationRequestService.ShowUserVacationRequest(userId);
                return result;
            }
            return null;
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Manager")]
        public List<UserVacationRequestDTO> ShowUserVacationRequestForManager()
        {
            var userId = User.FindFirst("id").Value;
            AppUser user = _userManager.FindByIdAsync(userId).Result;
            var check = _userManager.IsInRoleAsync(user, "Manager");
            if (check.Result)
            {
                var result = _vacationRequestService.ShowUserVacationRequestForManager(user);
                return result;
            }

            return null;
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "Employee")]
        public IActionResult CreateVacationRequest(UserVacationRequestDTO newVacationRequest)
        {
            if (!ModelState.IsValid || newVacationRequest.StartDate < DateTime.Now)
            {
                return BadRequest(Errors.AddErrorToModelState("vacationRequestError", "Invalid data, please try again", ModelState));
            }
            var vacationRequest = _vacationRequestService.CreateVacationRequest(newVacationRequest);
            if (vacationRequest != null)
            {
                return new OkObjectResult(vacationRequest);
            }
            return BadRequest(Errors.AddErrorToModelState("vacationRequestError", "You do not have so many vacation days, or invalid DateTime.Please check the data and try again", ModelState));
        }

        [HttpDelete("[action]/{vacationRequestId}")]
        [Authorize(Roles = "Employee")]
        public ResponseForRequest DeleteUserVacationRequest(int vacationRequestId)
        {
            //change this
            return _vacationRequestService.DeleteVacationRequest(vacationRequestId);
            
        }
    }
}