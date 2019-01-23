using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface ITeamRepository: IGenericRepository<Team>
    {
        List<Team> FindByManager(string idManager);
    }
}
