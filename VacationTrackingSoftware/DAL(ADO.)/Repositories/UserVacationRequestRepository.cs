using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class UserVacationRequestRepository : IUserVacationRequestRepository
    {
        public void Create(UserVacationRequest entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserVacationRequest entity)
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> FindForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> GetAllWithTypeHolidays()
        {
            throw new NotImplementedException();
        }

        public UserVacationRequest GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> GetForListOfUsers(List<AppUser> users)
        {
            throw new NotImplementedException();
        }

        public UserVacationRequest GetWithWorker(DateTime startDate, DateTime endDate, string userId)
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

        public void Update(UserVacationRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
