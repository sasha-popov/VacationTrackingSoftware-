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

        public List<Team> FindByManager(string idManager)
        {
            return RepositoryContext.Teams.Include(x => x.Manager).Where(x => x.Manager.Id == idManager).ToList();
        }
    }
}
