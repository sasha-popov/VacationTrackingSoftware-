using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public List<Team> AllTeamsWithManager()
        {
            throw new NotImplementedException();
        }

        public List<Team> FindByListIdTeam(List<int> teamsId)
        {
            throw new NotImplementedException();
        }

        public List<Team> FindByManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<Team> FindTeamsByManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<Team> FindTeamsByManagerForUpdate(string managerId)
        {
            throw new NotImplementedException();
        }
    }
}
