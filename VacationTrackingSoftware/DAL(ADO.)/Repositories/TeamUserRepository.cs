using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class TeamUserRepository : ITeamUserRepository
    {
        public void Create(TeamUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TeamUser entity)
        {
            throw new NotImplementedException();
        }

        public TeamUser FindByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> FindForManager(string managerId)
        {
            throw new NotImplementedException();
        }

        public List<TeamUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TeamUser> GetAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public TeamUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TeamUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
