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

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private readonly UserManager<AppUser> _userManager;
        private IWorkerRepository _workerRepository;
        public EmployeeController(IEmployeeService employeeService, IUserVacationRequestRepository userVacationRequestRepository, UserManager<AppUser> userManager, IWorkerRepository workerRepository)
        {
            _employeeService = employeeService;
            _userVacationRequestRepository = userVacationRequestRepository;
            _userManager = userManager;
            _workerRepository = workerRepository;
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
            else {
                return BadRequest(Errors.AddErrorToModelState("vacationRequestError", "You do not have so many vacation days, or invalid DateTime.Please check the data and try again", ModelState));
            }

        }

        [HttpGet("[action]")]
        public List<UserVacationRequestDTO> ShowUserVacationRequest()
        {
            var userId = User.FindFirst("id").Value;
            var result = _employeeService.ShowUserVacationRequest(userId);
            return result;
        }

        [HttpGet("[action]")]
        public List<UserVacationRequestDTO> ShowUserVacationRequestForManager()
        {
            var userId = User.FindFirst("id").Value;
            AppUser user = _userManager.FindByIdAsync(userId).Result;
            if (_userManager.IsInRoleAsync(user, "Manager").Result)
            {
                var result= _employeeService.ShowUserVacationRequestForManager(user);
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

    }
}