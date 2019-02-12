using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BLL.Services
{
    public class WorkerService : IWorkerService
    {
        private IWorkerRepository _workerRepository;
        private ITeamRepository _teamRepository;
        private ITeamUserRepository _teamUserRepository;
        public WorkerService(
            IWorkerRepository workerRepository,
            ITeamRepository teamRepository,
            ITeamUserRepository teamUserRepository)
        {
            _workerRepository = workerRepository;
            _teamRepository = teamRepository;
            _teamUserRepository = teamUserRepository;
        }

        public void CreateWorkerAndTeamUser(AppUser user, int teamId)
        {
            _workerRepository.Create(new Worker { DateRecruitment = DateTime.Now, User = user });

            _teamUserRepository.Create(new TeamUser
            {
                Team = _teamRepository.GetById(teamId),
                User = user
            });

            _teamUserRepository.Save();
        }
        internal string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }

        public ResponseForRequest SendDataToWorker(string email, string nickName, string password)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("tansked2000@gmail.com", "control1996")
                };

                using (var message = new MailMessage("tansked2000@gmail.com", email)
                {
                    Subject = "Login and password",
                    Body = "Your nickName:" + nickName + ", and your password:" + password
                })
                {
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Incorrecr email. Please try again!" } };
            }

            return new ResponseForRequest() { Successful = true };

        }
    }
}
