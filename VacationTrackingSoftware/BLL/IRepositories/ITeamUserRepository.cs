using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface ITeamUserRepository: IGenericRepository<TeamUser>
    {
        List<TeamUser> FindForManager(string idManager);
        //List<Team> FindTeamUserByManager(string idManager);
    }
}
