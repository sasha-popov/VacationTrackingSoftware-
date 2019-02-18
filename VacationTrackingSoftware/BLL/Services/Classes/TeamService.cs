using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
using BLL.Services.Interfaces;

namespace BLL.Services.Classes
{
    public class TeamService:ITeamService
    {
        private ITeamRepository _teamRepository;
        private ITeamUserRepository _teamUserRepository;
        public TeamService(
            ITeamRepository teamRepository,
            ITeamUserRepository teamUserRepository)
        {
            _teamRepository = teamRepository;
            _teamUserRepository = teamUserRepository;
        }
        public List<Team> GetAllTeams()
        {
            return _teamRepository.GetAll();
        }

        public ResponseForRequest UpdateTeamForWorker(AppUser user, int teamId = 0)
        {
            Team team = _teamRepository.GetById(teamId);
            TeamUser teamUser = _teamUserRepository.FindByUser(user.Id);

            if (teamUser == null)
            {
                _teamUserRepository.Create(new TeamUser() { Team = team, User = user });
            }
            else
            {
                teamUser.Team = team;
                _teamUserRepository.Update(teamUser);
            }
            _teamUserRepository.Save();
            return new ResponseForRequest() { Successful = true };
        }
        public ResponseForRequest CreateOrUpdateTeams(AppUser user, List<int> teamIds)
        {
            List<Team> oldTeams = _teamRepository.FindTeamsByManagerForUpdate(user.Id);

            foreach (var team in oldTeams) {
                team.Manager = null;
                _teamRepository.Update(team);
            }
            _teamRepository.Save();
            List<Team> newTeams = _teamRepository.FindByListIdTeam(teamIds);
            newTeams.ForEach(x =>
            {
                x.Manager = user;
                _teamRepository.Update(x);
            });
            _teamRepository.Save();   

            return new ResponseForRequest() { Successful = true };
        }
    }
}
