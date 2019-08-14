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
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).FirstOrDefault(x => x.User.Id == userId);
        }

        public List<AppUser> FindForManager(string managerId)
        {
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).Where(x=>x.Team.Manager.Id== managerId).Select(x => x.User).ToList();
        }

        public List<TeamUser> GetAllWithDetails()
        {
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).ToList();
        }
        public TeamUser GetByIdDA(int id)
        {
            throw new NotImplementedException();
        }
    }
}
