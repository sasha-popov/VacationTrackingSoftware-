﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Statuses;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private IVacationTypeRepository _vacationTypeRepository;
        private IVacationPolicyRepository _vacationPolicyRepository;
        private ICompanyHolidayRepository _companyHolidayRepository;
        private IWorkerRepository _workerRepository;
        private ITeamRepository _teamRepository;
        private UserManager<AppUser> _userManager;
        private ITeamUserRepository _teamUserRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IUserVacationRequestRepository userVacationRequestRepository,
            IVacationTypeRepository vacationTypeRepository,
            IVacationPolicyRepository vacationPolicyRepository,
            ICompanyHolidayRepository companyHolidayRepository,
            IWorkerRepository workerRepository,
            ITeamRepository teamRepository,
            ITeamUserRepository teamUserRepository ,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _userVacationRequestRepository = userVacationRequestRepository;
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            _companyHolidayRepository = companyHolidayRepository;
            _workerRepository = workerRepository;
            _teamRepository = teamRepository;
            _userManager = userManager;
            _teamUserRepository = teamUserRepository;
            _mapper = mapper;
        }


        public UserVacationRequestDTO CreateVacationRequest(UserVacationRequestDTO userVacationRequestDTO)
        {
            UserVacationRequestDTO response;
            Mapper.Reset();
            Mapper.Initialize(x => x.CreateMap<UserVacationRequestDTO, UserVacationRequest>()
            .ForMember("VacationType", opt => opt.MapFrom(c => _vacationTypeRepository.FindByCondition(y => y.Name == userVacationRequestDTO.VacationType).First()))
            .ForMember("User", opt => opt.MapFrom(user => _userManager.FindByIdAsync(userVacationRequestDTO.UserId).Result))
            .ForMember("Status", opt => opt.MapFrom(statuses => 1))
            );
            var result = Mapper.Map<UserVacationRequestDTO, UserVacationRequest>(userVacationRequestDTO);

            if (userVacationRequestDTO.StartDate < userVacationRequestDTO.EndDate)
            {
                List<bool> checkOverlaps = CheckDublicate(result);
                var daysForVacation = CheckVacationPolicies(result);
                if (!checkOverlaps.Contains(true) && daysForVacation != null)
                {
                    result.Payment = daysForVacation.Payments;
                    _userVacationRequestRepository.Create(result);
                    _userVacationRequestRepository.Save();
                    userVacationRequestDTO.Payment = daysForVacation.Payments;
                    response = userVacationRequestDTO;
                }
                else
                {
                    response = null;
                }
            }
            else
            { response = null; }

            return response;

        }

        public List<UserVacationRequestDTO> ShowUserVacationRequest(string id)
        {
            var userVacationRequests = _userVacationRequestRepository.FindForUser(id).ToList();
            var result = _mapper.Map<List<UserVacationRequestDTO>>(userVacationRequests);
            return result;
        }
        public List<UserVacationRequestDTO> ShowUserVacationRequestForManager(AppUser user)
        {
            //in coments it is bad aproach
            List<AppUser> UsersOfManager = _teamUserRepository.FindForManager(user.Id).Select(x => x.User).ToList();

            
            List<UserVacationRequest> userVacationRequestsForManager = _userVacationRequestRepository.GetForListOfUsers(UsersOfManager);
            //optional
            var actualRequests = userVacationRequestsForManager.Where(x => x.StartDate > DateTime.Now);
             var result = _mapper.Map<List<UserVacationRequestDTO>>(actualRequests);
            return result;
        }


        private List<bool> CheckDublicate(UserVacationRequest newRequest)
        {
            var currentRequests = _userVacationRequestRepository.FindForUser(newRequest.User.Id).ToList();

            List<bool> checkOverlaps = new List<bool>();
            if (currentRequests.Count == 0)
            {
                checkOverlaps.Add(false);
            }
            else
            {
                foreach (var request in currentRequests)
                {
                    checkOverlaps.Add((request.StartDate.Day <= newRequest.EndDate.Day && newRequest.StartDate.Day <= request.EndDate.Day));
                }
            }
            return checkOverlaps;
        }

        private CountOfVacationDTO CheckVacationPolicies(UserVacationRequest newrequest)
        {
            //check it
            var allvacations = _userVacationRequestRepository.FindForUser(newrequest.User.Id).Where(x => (x.StartDate.Year == 2019) && (x.VacationType.Name == newrequest.VacationType.Name)).ToList();
            var allHolidays = _companyHolidayRepository.FindByCondition(x => x.Date.Year == DateTime.Now.Year);
            List<DateTime> allDatesPrev = new List<DateTime>();

            if (allvacations.Count != 0)
            {
                //count prev days without sat and sun
                foreach (var vacation in allvacations)
                {
                    for (DateTime date = vacation.StartDate; date <= vacation.EndDate; date = date.AddDays(1))
                        if (date.DayOfWeek.ToString() != "Saturday" && date.DayOfWeek.ToString() != "Sunday")
                        {
                            allDatesPrev.Add(date);
                        }
                }
                //count prev days without company holidays
                allDatesPrev = GetListDaysWithoutCompanyHolidays(allDatesPrev);
            }

            //count days of current request
            List<DateTime> allDatesForCurrentRequest = new List<DateTime>();
            for (DateTime date = newrequest.StartDate; date <= newrequest.EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek.ToString() != "Saturday" && date.DayOfWeek.ToString() != "Sunday")
                {
                    allDatesForCurrentRequest.Add(date);
                }
            }
            //count days of current request without company holiday
            allDatesForCurrentRequest = GetListDaysWithoutCompanyHolidays(allDatesForCurrentRequest);

            //get vacation policy for category        
            List<VacationPolicy> currentVacationPolicy = _vacationPolicyRepository.FindCurrentVacationPolicy(newrequest);
            int commonCountOfday = currentVacationPolicy[0].Count + currentVacationPolicy[0].Count;

            //it check if is dates yet
            int countAllDatesForCurrentRequest = allDatesForCurrentRequest.Count;
            int countAllDatesPrev = allDatesPrev.Count;
            if (commonCountOfday >= countAllDatesForCurrentRequest + countAllDatesPrev)
            {
                VacationPolicy PolicyWithPay = currentVacationPolicy.Find(x => x.Payments == x.Count);
                int remainderPayDays = PolicyWithPay.Count - (countAllDatesForCurrentRequest + countAllDatesPrev);

                if (remainderPayDays >= 0)
                {
                    return new CountOfVacationDTO { Payments = countAllDatesForCurrentRequest, Free = 0 };
                }
                else if (countAllDatesPrev >= PolicyWithPay.Count)
                {
                    return new CountOfVacationDTO { Payments = 0, Free = countAllDatesForCurrentRequest };
                }
                else if (countAllDatesPrev <= PolicyWithPay.Count)
                {
                    return new CountOfVacationDTO { Payments = allDatesForCurrentRequest.Count + remainderPayDays, Free = -remainderPayDays };
                }
            }
            return null;
        }

        private List<DateTime> GetListDaysWithoutCompanyHolidays(List<DateTime> allDateTimes)
        {
            var allHolidays = _companyHolidayRepository.FindByCondition(x => x.Date.Year == DateTime.Now.Year);
            List<DateTime> result= new List<DateTime>();
            foreach (var checkHoliday in allHolidays)
            {
                var item = allDateTimes.SingleOrDefault(x => x.DayOfYear == checkHoliday.Date.DayOfYear);
                if (item != null)
                {
                    allDateTimes.Remove(item);
                }
            }
            return allDateTimes;
        }



    }
}
