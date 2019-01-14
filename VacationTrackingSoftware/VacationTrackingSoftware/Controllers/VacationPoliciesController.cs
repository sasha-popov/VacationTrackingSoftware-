using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class VacationPoliciesController : ControllerBase
    {
        private IVacationTypeRepository _vacationTypeRepository;
        private IVacationPolicyRepository _vacationPolicyRepository;
        private IUserRepository _userRepository;
        private IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private IVacationPoliciesService _vacationPoliciesService;


        public VacationPoliciesController(IMapper mapper,
                                         IVacationTypeRepository vacationTypeRepository, 
                                         IVacationPolicyRepository vacationPolicyRepository, 
                                         IUserRepository userRepository,
                                         IEmployeeService employeeService,
                                         IVacationPoliciesService vacationPoliciesService)
        {
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            _userRepository = userRepository;
            _employeeService = employeeService;
            _mapper = mapper;
            _vacationPoliciesService = vacationPoliciesService;
        }
        [HttpGet("[action]")]
        public List<VacationType> GetTypesOfVacation()
        {
            return _vacationTypeRepository.GetAll().ToList();
        }

        [HttpPost("[action]")]
        public void SendVacationPolicy(VacationPolicyDTO newVacationPolicy)
        {
            if (ModelState.IsValid)
            {
                _vacationPoliciesService.SendVacationPolicy(newVacationPolicy);
            }
        }

        [HttpGet("[action]")]
        public List<VacationPolicyDTO> GetVacationPolicies()
        {
            return _vacationPoliciesService.GetVacationPolicies();
        }

        [HttpDelete("[action]/{years}/{vacationType}/{payments}")]
        public void DeleteVacationPolicy(int years, string vacationType, int payments)
        {
            _vacationPoliciesService.DeleteVacationPolicy(years, vacationType, payments);
        }


    }
}