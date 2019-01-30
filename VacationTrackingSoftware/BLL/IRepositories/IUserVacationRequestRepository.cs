using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface IUserVacationRequestRepository : IGenericRepository<UserVacationRequest>
    {
        UserVacationRequest GetWithWorker(DateTime startDate, DateTime endDate, string userId);

        List<UserVacationRequest> GetAllWithTypeHolidays();

        List<UserVacationRequest> GetForListOfUsers(List<AppUser> users);
        IEnumerable<UserVacationRequest> FindForUser(string userId);
    }
}
