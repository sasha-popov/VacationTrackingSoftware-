using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return RepositoryContext.Teams.Where(x => teamsId.Contains(x.Id)).ToList();
        }

        public List<Team> FindTeamsByManager(string managerId)
        {
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
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            var getTeamssOfManager = RepositoryContext.Teams.Where(x => x.Manager.Id == managerId).ToList();
            sWatch.Stop();
            var resNotP = sWatch.ElapsedMilliseconds.ToString();
            //for testing Parallel
            //Stopwatch sWatchP = new Stopwatch();
            //sWatchP.Start();
            //var parallel = RepositoryContext.Teams.AsParallel().Where(x => x.Manager.Id == managerId).ToList();
            //sWatchP.Stop();
            //var resP = sWatchP.ElapsedMilliseconds.ToString();
            return getTeamssOfManager;
        }
    }
}
