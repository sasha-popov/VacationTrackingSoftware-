using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserVacationRequestRepository : GenericRepository<UserVacationRequest>, IUserVacationRequestRepository
    {
        public UserVacationRequestRepository(ProjectContext context) : base(context) { }

        public List<UserVacationRequest> GetForListOfUsers(List<AppUser> users)
        {
            var allRequest = this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType);
            var result = allRequest.Where(x => users.Contains(x.User)).ToList();
            return result;
        }

        public List<UserVacationRequest> FindForUser(string userId)
        {
            var allRequest = this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType).Where(x => x.User.Id == userId).ToList();
            return allRequest;
        }


        public List<UserVacationRequest> GetAllWithTypeHolidays()
        {
            return this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType).ToList();
        }


        public UserVacationRequest GetWithWorker(DateTime startDate, DateTime endDate, string userId)
        {
            return RepositoryContext.UserVacantionRequests.Include(x => x.User).FirstOrDefault(x => x.StartDate == startDate && x.EndDate == endDate && x.User.Id == userId);
        }
    }
}
