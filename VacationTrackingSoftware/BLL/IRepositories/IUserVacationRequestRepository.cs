using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface IUserVacationRequestRepository : IGenericRepository<UserVacationRequest>
    {
        List<UserVacationRequest> GetAllWithTypeHolidays();

        List<UserVacationRequest> GetForListOfUsers(List<AppUser> users);
        List<UserVacationRequest> FindForUser(string userId);
    }
}
