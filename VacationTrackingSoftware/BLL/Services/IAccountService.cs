using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using BLL.Result;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services
{
   public interface IAccountService
    {
        void CreateWorkerAndTeamUser(AppUser user, int teamId);
        void CreateWorkerAndUpdateTeams(AppUser user, List<int> teamsId);
        List<Team> GetAllTeams();
        ResponseForRequest UpdateUserTeam(AppUser user,string role, int teamId = 0, List<int> teamsId = null);
    }
}
