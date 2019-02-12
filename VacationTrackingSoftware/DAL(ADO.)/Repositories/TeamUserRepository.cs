using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class TeamUserRepository : GenericRepository<TeamUser>, ITeamUserRepository
    {
        public TeamUser FindByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> FindForManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<TeamUser> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }
    }
}
