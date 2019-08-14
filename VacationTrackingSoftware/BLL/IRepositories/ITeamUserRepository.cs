using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface ITeamUserRepository: IGenericRepository<TeamUser>
    {
        List<AppUser> FindForManager(string managerId);

        List<TeamUser> GetAllWithDetails();

        TeamUser FindByUser(string userId);

        TeamUser GetByIdDA(int id);
    }
}
