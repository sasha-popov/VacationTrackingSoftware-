using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using BLL.Result;

namespace BLL.Services.Interfaces
{
    public interface ITeamService
    {
        List<Team> GetAllTeams();
        ResponseForRequest UpdateTeamForWorker(AppUser user, int teamId = 0);
        ResponseForRequest CreateOrUpdateTeams(AppUser user, List<int> teamsId);
    }
}
