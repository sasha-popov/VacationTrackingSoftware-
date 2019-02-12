using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class VacationRequestService : IVacationRequestService
    {
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private IVacationTypeRepository _vacationTypeRepository;
        private IVacationPolicyRepository _vacationPolicyRepository;
        private ICompanyHolidayRepository _companyHolidayRepository;
        private UserManager<AppUser> _userManager;
        private ITeamUserRepository _teamUserRepository;
        private readonly IMapper _mapper;

        public VacationRequestService(
            IUserVacationRequestRepository userVacationRequestRepository,
            IVacationTypeRepository vacationTypeRepository,
            IVacationPolicyRepository vacationPolicyRepository,
            ICompanyHolidayRepository companyHolidayRepository,
            ITeamUserRepository teamUserRepository,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _userVacationRequestRepository = userVacationRequestRepository;
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            _companyHolidayRepository = companyHolidayRepository;
            _userManager = userManager;
            _teamUserRepository = teamUserRepository;
            _mapper = mapper;
        }


        public UserVacationRequestDTO CreateVacationRequest(UserVacationRequestDTO userVacationRequestDTO)
        {
            Mapper.Reset();
            Mapper.Initialize(x => x.CreateMap<UserVacationRequestDTO, UserVacationRequest>()
            .ForMember("VacationType", opt => opt.MapFrom(c => _vacationTypeRepository.FindByName(userVacationRequestDTO.VacationType)))
            .ForMember("User", opt => opt.MapFrom(user => _userManager.FindByIdAsync(userVacationRequestDTO.UserId).Result))
            .ForMember("Status", opt => opt.MapFrom(statuses => 1))
            );
            var result = Mapper.Map<UserVacationRequestDTO, UserVacationRequest>(userVacationRequestDTO);

            if (userVacationRequestDTO.StartDate < userVacationRequestDTO.EndDate)
            {
                bool checkOverlaps = CheckDublicate(result);
                var daysForVacation = CheckVacationPolicies(result);
                if (checkOverlaps!=true && daysForVacation != null)
                {
                    result.Payment = daysForVacation.Payments;
                    _userVacationRequestRepository.Create(result);
                    _userVacationRequestRepository.Save();
                    userVacationRequestDTO.Payment = daysForVacation.Payments;
                    return userVacationRequestDTO;
                }                
            }
            return null;
        }
        public List<UserVacationRequestDTO> ShowUserVacationRequest(string id)
        {
            var userVacationRequests = _userVacationRequestRepository.FindForUser(id).ToList();
            var result = _mapper.Map<List<UserVacationRequestDTO>>(userVacationRequests);
            return result;
        }
        public List<UserVacationRequestDTO> ShowUserVacationRequestForManager(AppUser user)
        {
            //in comments it is bad aproach
            List<AppUser> UsersOfManager = _teamUserRepository.FindForManager(user.Id);
            List<UserVacationRequest> userVacationRequestsForManager = _userVacationRequestRepository.GetForListOfUsers(UsersOfManager);
            //optional
            var result = _mapper.Map<List<UserVacationRequestDTO>>(userVacationRequestsForManager);
            return result;
        }


        private bool CheckDublicate(UserVacationRequest newRequest)
        {
            var currentRequests = _userVacationRequestRepository.FindForUser(newRequest.User.Id);
            if (!currentRequests.Any())
            {
                return false;
            }
            else
            {
                foreach (var request in currentRequests)
                {
                    if(request.StartDate.Day <= newRequest.EndDate.Day && newRequest.StartDate.Day <= request.EndDate.Day) return true;
                }
            }
            return false;
        }

        private CountOfVacationDTO CheckVacationPolicies(UserVacationRequest newrequest)
        {
            //check it
            var allvacations = _userVacationRequestRepository.FindForUser(newrequest.User.Id)
                    .Where(x => (x.StartDate.Year == 2019) && (x.VacationType.Name == newrequest.VacationType.Name)).ToList();
            List<DateTime> allDatesPrev = new List<DateTime>();

            if (allvacations.Any())
            {
                //count prev days without sat and sun
                foreach (var vacation in allvacations)
                {
                    for (DateTime date = vacation.StartDate; date <= vacation.EndDate; date = date.AddDays(1))
                        if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
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
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    allDatesForCurrentRequest.Add(date);
                }
            }
            //count days of current request without company holiday
            allDatesForCurrentRequest = GetListDaysWithoutCompanyHolidays(allDatesForCurrentRequest);

            //get vacation policy for category        
            List<VacationPolicy> currentVacationPolicy = _vacationPolicyRepository.FindCurrentVacationPolicy(newrequest);
            int commonCountOfday=0;
            try
            {
                commonCountOfday = currentVacationPolicy[0].Count + currentVacationPolicy[1].Count;
            }
            catch {
                return null;
            }
            
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
            var allHolidays = _companyHolidayRepository.GetAllHolidaysForCurrentYear();
            List<DateTime> result = new List<DateTime>();
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
