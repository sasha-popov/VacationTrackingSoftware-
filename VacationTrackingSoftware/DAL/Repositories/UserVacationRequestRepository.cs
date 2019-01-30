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

        public IEnumerable<UserVacationRequest> FindByConditionWithUser(Expression<Func<UserVacationRequest, bool>> expression)
        {
            var result= this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType).Where(expression);
            return result;
        }

        public IEnumerable<UserVacationRequest> FindForUser(string userId)
        {
            var result = this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType).Where(x => x.User.Id == userId);
            return result;
        }


        public List<UserVacationRequest> GetAllWithTypeHolidays()
        {
            return this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType).ToList();
        }

        public List<UserVacationRequest> GetForListOfUsers(List<AppUser> users)
        {
            var result= this.RepositoryContext.UserVacantionRequests.Include(x => x.User).Include(x => x.VacationType).Where(x => users.Contains(x.User)).ToList();
            return result;
        }

        public UserVacationRequest GetWithWorker(DateTime startDate, DateTime endDate, string userId)
        {
            return RepositoryContext.UserVacantionRequests.Include(x => x.User).FirstOrDefault(x => x.StartDate == startDate && x.EndDate == endDate && x.User.Id == userId);
        }
    }
}
