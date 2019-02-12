using System;
using System.Collections.Generic;
using System.Text;
using BLL.Result;

namespace BLL.Services
{
   public interface IManagerService
    {
        void CreateWorkerAndUpdateTeams(AppUser user, List<int> teamIds = null);
    }
}
