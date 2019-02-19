using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.IRepositories
{
    public interface ITeamRepository: IGenericRepository<Team>
    {
        List<Team> FindByListIdTeam(List<int> teamsId);

        List<Team> FindTeamsByManager(string managerId);

        List<Team> AllTeamsWithManager();
        List<Team> FindTeamsByManagerForUpdate(string managerId);
    }
}
