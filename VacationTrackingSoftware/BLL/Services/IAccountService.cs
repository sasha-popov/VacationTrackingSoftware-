using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;

namespace BLL.Services
{
   public interface IAccountService
    {
        void CreateWorkerAndTeamUser(AppUser user, int teamId);
        void CreateWorkerAndUpdateTeams(AppUser user, List<int> teamsId);
        List<Team> GetAllTeams();
    }
}
