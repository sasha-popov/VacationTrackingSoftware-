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
        public void CreateWorkerAndUpdateTeams(AppUser user, List<int> teamIds = null)
        {
            Worker Worker = new Worker() { DateRecruitment = DateTime.Now, User = user };
            _workerRepository.Create(Worker);

            List<Team> oldTeams = _teamRepository.FindTeamsByManagerForUpdate(user.Id);
            oldTeams.ForEach(x =>
            {
                x.Manager = null;
                _teamRepository.Update(x);
            });
            _teamRepository.Save();
            List<Team> newTeams = _teamRepository.FindByListIdTeam(teamIds);
            newTeams.ForEach(x =>
            {
                x.Manager = user;
                _teamRepository.Update(x);
            });
            _teamRepository.Save();           
        }

    }
}
