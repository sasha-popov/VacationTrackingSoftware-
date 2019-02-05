using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.Helpers;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VacationPoliciesController : ControllerBase
    {
        private IVacationTypeRepository _vacationTypeRepository;
        private IVacationPolicyRepository _vacationPolicyRepository;
        private readonly IMapper _mapper;
        private IVacationPoliciesService _vacationPoliciesService;


        public VacationPoliciesController(IMapper mapper,
                                         IVacationTypeRepository vacationTypeRepository, 
                                         IVacationPolicyRepository vacationPolicyRepository, 
                                         IVacationPoliciesService vacationPoliciesService)
        {
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            _mapper = mapper;
            _vacationPoliciesService = vacationPoliciesService;
        }
        [HttpGet("[action]")]
        public List<VacationType> GetTypesOfVacation()
        {
            return _vacationTypeRepository.GetAll().ToList();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "HrUser")]
        public IActionResult SendVacationPolicy(VacationPolicyDTO newVacationPolicy)
        {
            bool result;
            if (ModelState.IsValid && newVacationPolicy.Count>=newVacationPolicy.Payments)
            {
                result=_vacationPoliciesService.SendVacationPolicy(newVacationPolicy);
                if (result == false)
                {
                    return BadRequest(Errors.AddErrorToModelState("vacationPolicyError", "This fields is not available", ModelState));
                }
                else {
                    return new OkObjectResult("Vacation policy create");
                } 
            }
            return BadRequest(Errors.AddErrorToModelState("vacationPolicyError", "This fields is not available", ModelState));
        }

        [HttpGet("[action]")]
        public List<VacationPolicyDTO> GetVacationPolicies()
        {
            return _vacationPoliciesService.GetVacationPolicies();
        }

        [HttpDelete("[action]/{years}/{vacationType}/{payments}")]
        [Authorize(Roles = "HrUser")]
        public void DeleteVacationPolicy(int years, string vacationType, int payments)
        {
            _vacationPoliciesService.DeleteVacationPolicy(years, vacationType, payments);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "HrUser")]
        public IActionResult UpdateVacationPolicy(VacationPolicyDTO vacationPolicy) {
            if (ModelState.IsValid && vacationPolicy.Count >= vacationPolicy.Payments)
            {
                VacationPolicy result = _mapper.Map<VacationPolicy>(vacationPolicy);
                result.VacationType = _vacationTypeRepository.FindByName(vacationPolicy.VacationType);
                _vacationPolicyRepository.Update(result);
                _vacationPolicyRepository.Save();
                return new OkObjectResult("Vacation policy have updated");
            }
            else {
                return BadRequest(Errors.AddErrorToModelState("vacationPolicyError", "This fields is not available", ModelState));
            }
        }


    }
}