﻿using System;
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

        public List<Team> FindByListIdTeam(int[] teamsId)
        {
            return RepositoryContext.Teams.Where(x => teamsId.Contains(x.Id)).ToList();
        }

        public List<Team> FindByManager(string idManager)
        {
            return RepositoryContext.Teams.Include(x => x.Manager).Include(x=>x.TeamUsers).Include("TeamUsers.User").Where(x => x.Manager.Id == idManager).ToList();
        }
        public List<Team> FindTeamsByManager(string managerId)
        {
            //it need to change
            var getTeamssOfManager = RepositoryContext.Teams.Include(x => x.Manager).Include(x => x.TeamUsers).Include("TeamUsers.User").Where(x => x.Manager.Id == managerId).ToList();
            foreach (var team in getTeamssOfManager)
            {
                foreach (var teamsUser in team.TeamUsers)
                {
                    teamsUser.Team = null;
                }
            }
            return getTeamssOfManager;
        }
    }
}
