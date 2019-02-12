using System;
using System.Collections.Generic;
using System.Text;
using BLL.IRepositories;
using BLL.Models;

namespace DAL_ADO._.Repositories
{
    public class UserVacationRequestRepository : GenericRepository<UserVacationRequest>, IUserVacationRequestRepository
    {
        public List<UserVacationRequest> FindForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<UserVacationRequest> GetAllWithTypeHolidays()
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
    }
}
