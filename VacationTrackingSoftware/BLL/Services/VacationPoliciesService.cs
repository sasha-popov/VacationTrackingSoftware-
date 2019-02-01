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
        //private IUserRepository _userRepository;
        private IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public VacationPoliciesService(IMapper mapper, IVacationTypeRepository vacationTypeRepository, IVacationPolicyRepository vacationPolicyRepository, 
            //IUserRepository userRepository,
            UserManager<AppUser> userManager,
            IEmployeeService employeeService)
        {
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            //_userRepository = userRepository;
            _employeeService = employeeService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public bool SendVacationPolicy(VacationPolicyDTO newVacationPolicy)
        {
            VacationPolicy result = _mapper.Map<VacationPolicy>(newVacationPolicy);
            //var hrUser =_userManager.FindByIdAsync(newVacationPolicy.UserId).Result;
            //result.HrUser=hrUser;
            //add validation
            result.VacationType = _vacationTypeRepository.FindByName(newVacationPolicy.VacationType);
            _vacationPolicyRepository.Create(result);
            _vacationPolicyRepository.Save();
            return true;
        }

        public List<VacationPolicyDTO> GetVacationPolicies()
        {
            List<VacationPolicy> allVacationPolicy = _vacationPolicyRepository.GetAllVacationPoliciesWithTypes().ToList();
            var resultDTO = _mapper.Map<List<VacationPolicyDTO>>(allVacationPolicy);
            return resultDTO;
        }

        public void DeleteVacationPolicy(int years, string vacationType, int payments)
        {
            var currentVacationPolicy = _vacationPolicyRepository.FindForDelete(years, vacationType, payments);
            _vacationPolicyRepository.Delete(currentVacationPolicy);
            _vacationPolicyRepository.SaveAsync();
        }
    }
}
