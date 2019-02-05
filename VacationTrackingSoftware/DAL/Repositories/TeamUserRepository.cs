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
    public class TeamUserRepository : GenericRepository<TeamUser>, ITeamUserRepository
    {
        public TeamUserRepository(ProjectContext context) : base(context) { }

        public TeamUser FindByUser(string userId)
        {
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).Where(x => x.User.Id == userId).FirstOrDefault();
        }

        public List<TeamUser> FindForManager(string idManager)
        {
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).Where(x=>x.Team.Manager.Id==idManager).ToList();
        }

        public List<TeamUser> GetAllWithDetails()
        {
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).ToList();
        }
    }
}
