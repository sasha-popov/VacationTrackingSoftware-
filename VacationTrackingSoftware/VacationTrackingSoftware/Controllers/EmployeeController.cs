using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IUserVacationRequestRepository _userVacationRequestRepository;
        public EmployeeController(IEmployeeService employeeService, IUserVacationRequestRepository userVacationRequestRepository)
        {
            _employeeService = employeeService;
            _userVacationRequestRepository = userVacationRequestRepository;
        }

        [HttpPost("[action]")]
        public void CreateVacationRequest(UserVacationRequestDTO newVacationRequest)
        {
            _employeeService.CreateVacationRequest(newVacationRequest);

        }

        [HttpGet("[action]/{id}")]
        public List<UserVacationRequestDTO> ShowUserVacationRequest(int id)
        {
            var result = _employeeService.ShowUserVacationRequest(id);
            return result;
        }

        [HttpDelete("[action]/{startDate}/{endDate}")]
        public void deleteUserVacationRequest(DateTime startDate, DateTime endDate)
        {
            //change this
            var currentVacationRequest = _userVacationRequestRepository.FindByCondition(x => x.StartDate==startDate && x.EndDate==endDate).First();
            _userVacationRequestRepository.Delete(currentVacationRequest);
            _userVacationRequestRepository.Save();
        }

    }
}