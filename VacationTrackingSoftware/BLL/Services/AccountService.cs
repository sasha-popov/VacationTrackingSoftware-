using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace BLL.Services
{
   public class AccountService:IAccountService 
    {
        private IUserVacationRequestRepository _userVacationRequestRepository;
        private IVacationTypeRepository _vacationTypeRepository;
        private IVacationPolicyRepository _vacationPolicyRepository;
        private ICompanyHolidayRepository _companyHolidayRepository;
        private IWorkerRepository _workerRepository;
        private ITeamRepository _teamRepository;
        private ITeamUserRepository _teamUserRepository;
        public AccountService(
            IUserVacationRequestRepository userVacationRequestRepository,
            IVacationTypeRepository vacationTypeRepository,
            IVacationPolicyRepository vacationPolicyRepository,
            ICompanyHolidayRepository companyHolidayRepository,
            IWorkerRepository workerRepository,
            ITeamRepository teamRepository,
            ITeamUserRepository teamUserRepository)
        {
            _userVacationRequestRepository = userVacationRequestRepository;
            _vacationTypeRepository = vacationTypeRepository;
            _vacationPolicyRepository = vacationPolicyRepository;
            _companyHolidayRepository = companyHolidayRepository;
            _workerRepository = workerRepository;
            _teamRepository = teamRepository;
            _teamUserRepository = teamUserRepository;
        }

        public void CreateWorkerAndTeamUser(AppUser user, int teamId, string role)
        {
            _workerRepository.Create(new Worker { DateRecruitment = DateTime.Now, User = user });
            _workerRepository.Save();
            Worker worker = _workerRepository.GetWithUser(user.Id);
            Team team = _teamRepository.GetById(teamId);
            if (team != null)
            {
                if (role == "Employee")
                {

                    TeamUser teamUser = new TeamUser { Team = team, User = user };
                    _teamUserRepository.Create(teamUser);
                    _teamUserRepository.Save();
                }
                else if (role == "Manager")
                {
                    team.Manager = user;
                    _teamRepository.Update(team);
                    _teamRepository.Save();
                }
            }

        }

        internal string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }
    }
}
