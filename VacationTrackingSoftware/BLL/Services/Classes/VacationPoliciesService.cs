using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
  public  class VacationPoliciesService: IVacationPoliciesService
    {
        private IVacationTypeRepository _vacationTypeRepository;
        private IVacationPolicyRepository _vacationPolicyRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public VacationPoliciesService(IMapper mapper, IVacationTypeRepository vacationTypeRepository, IVacationPolicyRepository vacationPolicyRepository, 
            UserManager<AppUser> userManager)
        {
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public bool SendVacationPolicy(VacationPolicyDTO newVacationPolicy)
        {
            VacationPolicy result = _mapper.Map<VacationPolicy>(newVacationPolicy);
            result.VacationType = _vacationTypeRepository.FindByName(newVacationPolicy.VacationType);
            _vacationPolicyRepository.Create(result);
            _vacationPolicyRepository.Save();
            return true;
        }

        public List<VacationPolicyDTO> GetVacationPolicies()
        {
            List<VacationPolicy> allVacationPolicy = _vacationPolicyRepository.GetAllVacationPoliciesWithTypes();
            var resultDTO = _mapper.Map<List<VacationPolicyDTO>>(allVacationPolicy);
            return resultDTO;
        }

        public void DeleteVacationPolicy(int vacationPolicyId)
        {
            var currentVacationPolicy = _vacationPolicyRepository.GetById(vacationPolicyId);
            _vacationPolicyRepository.Delete(currentVacationPolicy);
            _vacationPolicyRepository.Save();
        }
    }
}
