using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private IWorkerRepository _workerRepository;
        private ITeamRepository _teamRepository;
        private ITeamUserRepository _teamUserRepository;
        public AccountService(
            IWorkerRepository workerRepository,
            ITeamRepository teamRepository,
            ITeamUserRepository teamUserRepository)
        {
            _workerRepository = workerRepository;
            _teamRepository = teamRepository;
            _teamUserRepository = teamUserRepository;
        }

        public void CreateWorkerAndTeamUser(AppUser user, int teamId)
        {
            _workerRepository.Create(new Worker { DateRecruitment = DateTime.Now, User = user });

            _teamUserRepository.Create(new TeamUser
            {
                Team = _teamRepository.GetById(teamId),
                User = user
            });

            _teamUserRepository.Save();
        }

        public void CreateWorkerAndUpdateTeams(AppUser user, List<int> teamsId)
        {
            List<Team> teams = _teamRepository.FindByListIdTeam(teamsId);
            foreach (var team in teams)
            {
                team.Manager = user;
                _teamRepository.Update(team);
            }
            _teamRepository.Save();
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

        public List<Team> GetAllTeams()
        {
            return _teamRepository.GetAll().ToList();
        }

        public bool UpdateUserTeam(AppUser user, string role, int teamId = 0, List<int> teamsId = null)
        {
            if (role == "Employee") return UpdateEmployee(user, teamId);
            //if (role == "Manager") { };
            return false;
        }

        private bool UpdateEmployee(AppUser user, int teamId = 0)
        {
            if (teamId != 0)
            {
                Team team = _teamRepository.GetById(teamId);
                TeamUser teamUser = _teamUserRepository.FindByUser(user.Id);
                if (teamUser == null)
                {
                    _teamUserRepository.Create(new TeamUser() { Team = team, User = user });
                }
                else {
                    teamUser.Team = team;
                    _teamUserRepository.Update(teamUser);
                }
                _teamUserRepository.Save();
                return true;
            }
            return false;
        }

        private void UpdateManager(AppUser user, List<int> teamsId = null) {
            if (teamsId.Any())
            {
                List<Team> teams = _teamRepository.FindByListIdTeam(teamsId);
                teams.ForEach(team =>
                {
                    team.Manager = user;
                    _teamRepository.Update(team);
                });
            }
            else {

            }
        }
    }
}
