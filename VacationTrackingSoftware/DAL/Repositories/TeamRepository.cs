using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(ProjectContext context) : base(context) { }

        public List<Team> AllTeamsWithManager()
        {
            return RepositoryContext.Teams.AsNoTracking().Include(x => x.Manager).ToList();
        }

        public List<Team> FindByListIdTeam(List<int> teamsId)
        {
            return RepositoryContext.Teams.AsNoTracking().Where(x => teamsId.Contains(x.Id)).ToList();
        }

        public List<Team> FindTeamsByManager(string managerId)
        {
            //it need to change
            var getTeamssOfManager = RepositoryContext.Teams.AsNoTracking().Include(x => x.Manager).Include(x => x.TeamUsers).Include("TeamUsers.User").Where(x => x.Manager.Id == managerId).ToList();
            //this need because in json it is a cycle
            foreach (var team in getTeamssOfManager)
            {
                foreach (var teamsUser in team.TeamUsers)
                {
                    teamsUser.Team = null;
                }
            }
            return getTeamssOfManager;
        }

        public List<Team> FindTeamsByManagerForUpdate(string managerId)
        {
            var getTeamssOfManager = RepositoryContext.Teams.AsNoTracking().Include(x => x.Manager).Include(x => x.TeamUsers).Include("TeamUsers.User").Where(x => x.Manager.Id == managerId).AsNoTracking().ToList();
            return getTeamssOfManager;
        }
    }
}
