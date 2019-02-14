using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IRepositories;
using BLL.Models;
using BLL.Result;
using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.ViewModels;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private ITeamUserRepository _teamUserRepository;
        private ITeamRepository _teamRepository;
        private ITeamService _teamService;
        public TeamsController( 
            UserManager<AppUser> userManager,
            ITeamUserRepository teamUserRepository,
            ITeamRepository teamRepository,
            ITeamService teamService
            )
        {
            _userManager = userManager;
            _teamUserRepository = teamUserRepository;
            _teamRepository = teamRepository;
            _teamService = teamService;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Manager")]
        public List<Team> GetTeamsForManager()
        {
            var userId = User.FindFirst("id").Value;
            var result = _teamRepository.FindTeamsByManager(userId);
            return result;
        }

        //test
        [HttpPost("[action]")]
        public ResponseForRequest AddTeam()
        {
            Team team = new Team() { Manager = null, Name = "testAdo1" };
            _teamRepository.Create(team);
            return new ResponseForRequest() { Successful = true };
        }

        [HttpGet("[action]")]
        public List<Team> GetAllTeams()
        {
            return _teamService.GetAllTeams();
        }
        [HttpPut("[action]")]
        public ResponseForRequest UpdateUserTeam(UpdateUserTeam updateUserTeam)
        {
            AppUser userForUpdate = _userManager.FindByIdAsync(updateUserTeam.UserId).Result;
            try
            {
                if (updateUserTeam.Role == (int)Roles.Employee)
                {
                    return _teamService.UpdateTeamForWorker(userForUpdate, updateUserTeam.TeamId);
                }
                else
                {
                    return _teamService.CreateOrUpdateTeams(userForUpdate, updateUserTeam.TeamIds);
                }
            }
            catch
            {
                return new ResponseForRequest() { Successful = false, Errors = new List<string>() { "Inavalid data.Please try again" } };
            }
        }


        [HttpGet("[action]")]
        [Authorize(Roles = "HrUser")]
        public List<WorkersViewModel> GetALLWorkersForHrUser()
        {
            var allEmployee = _teamUserRepository.GetAllWithDetails();
            List<WorkersViewModel> workersViewModel = new List<WorkersViewModel>();
            allEmployee.ForEach(x =>
            {
                if (x.Team != null) x.Team.TeamUsers = null;
                workersViewModel.Add(new WorkersViewModel() { FirstName = x.User.FirstName, LastName = x.User.LastName, Id = x.User.Id, Team = x.Team, Role = (int)Roles.Employee });
            });

            List<AppUser> allManagers = _userManager.GetUsersInRoleAsync("Manager").Result.ToList();
            allManagers.ForEach(manager =>
            {
                workersViewModel.Add(new WorkersViewModel() { FirstName = manager.FirstName, Id = manager.Id, LastName = manager.LastName, Role = (int)Roles.Manager, Teams = new List<Team>() });
            });

            List<Team> teams = _teamRepository.AllTeamsWithManager().ToList();
            teams.ForEach(team =>
            {
                if (team.Manager != null)
                {
                    var result = workersViewModel.FirstOrDefault(x => x.Id == team.Manager.Id);
                    result.Teams.Add(team);
                }
            });

            return workersViewModel;
        }
    }
}