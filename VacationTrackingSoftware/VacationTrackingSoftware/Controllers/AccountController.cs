using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private IAccountService _accountService;

        public AccountController(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IAccountService accountService)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _accountService = accountService;
        }

        [HttpPost("[action]")]
        public void CreateEmployee(User employee)
        {
            _accountService.CreateEmployee(employee);
        }
        [HttpGet("[action]/{name}/{password}")]
        public UserRole Redirect(string name, string password) {
            //if have more one role, need to replace this code
            User currentUser = _userRepository.GetWithUserRoles(name, password);
            UserRole userRole = _userRoleRepository.GetWithAllObjects(currentUser.Id);
            userRole.Role.UserRoles = null;
            userRole.User.UserRoles = null;
            //it is not work
            return userRole;
        }
    }
}