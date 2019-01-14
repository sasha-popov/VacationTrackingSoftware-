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
        [HttpPost("[action]")]
        public ActionResult Redirect(UserDataDTO user) {
            User currentUser = _userRepository.GetWithUserRoles(user.Name, user.Password);
            UserRole userRole = _userRoleRepository.GetWithRole(currentUser.Id);
            //it is not work
            return new RedirectResult("/employee");
        }
    }
}