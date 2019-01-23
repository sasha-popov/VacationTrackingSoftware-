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

        public List<TeamUser> FindWithObjects()
        {
            return RepositoryContext.TeamUsers.Include(x => x.Team).Include(x => x.User).ToList();
        }
    }
}
