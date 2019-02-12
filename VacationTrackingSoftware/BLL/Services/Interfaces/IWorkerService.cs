using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using BLL.Result;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services
{
   public interface IWorkerService
    {
        void CreateWorkerAndTeamUser(AppUser user, int teamId);
        ResponseForRequest SendDataToWorker(string email, string nickName, string password);
    }
}
