using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;

namespace BLL.Services
{
    public class ManagerService:IManagerService
    {
        private ITeamRepository _teamRepository;
        private IWorkerRepository _workerRepository;
        public ManagerService(
            ITeamRepository teamRepository,IWorkerRepository workerRepository)
        {
            _teamRepository = teamRepository;
            _workerRepository = workerRepository;
        }
        public ResponseForRequest UpdateTeams(AppUser user, List<int> teamIds = null)
        {
            //change this method
            List<Team> oldTeams = _teamRepository.FindTeamsByManagerForUpdate(user.Id);

            foreach (var team in oldTeams)
            {
                var currentTeam = _teamRepository.GetById(team.Id);
                currentTeam.Manager = null;
                _teamRepository.Update(currentTeam);
                _teamRepository.Save();
            }

            List<Team> newTeams = _teamRepository.FindByListIdTeam(teamIds);
            foreach (var team in newTeams) {
                var currentTeam = _teamRepository.GetById(team.Id);
                currentTeam.Manager = user;
                _teamRepository.Update(currentTeam);
            }
            _teamRepository.Save();
            return new ResponseForRequest() { Successful = true };
        }

    }
}
