using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.IRepositories;
using BLL.Result;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VacationTrackingSoftware.ViewModels;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UpdateUserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWorkerRepository _workerRepository;
        public UpdateUserController(UserManager<AppUser> userManager, IWorkerRepository workerRepository) {
            _userManager = userManager;
            _workerRepository = workerRepository;
        }

        [HttpGet("[action]")]
        public async Task<AppUser> GetCurrentUser() {
            string userId = User.FindFirst("id").Value;
            AppUser appUser = _userManager.FindByIdAsync(userId).Result;
            return appUser;
        }

        [HttpPost("[action]")]
        public ResponseForRequest Update(UpdateUserDTO currentChanges)
        {
            string userId = User.FindFirst("id").Value;
            AppUser appUser = _userManager.FindByIdAsync(userId).Result;
            if (ModelState.IsValid)
            {
                appUser = AppUser.UpdateModel(appUser, currentChanges);
                try
                {
                    //UpdatePassword(appUser, currentChanges.Password);
                    _userManager.UpdateAsync(appUser);
                    _workerRepository.SaveAsync();
                    return new ResponseForRequest() { Successful = true };
                }
                catch(Exception ex)
                {
                    return new ResponseForRequest() { Successful = false, Errors = new List<string> { "Invalid datas, try again!" } };
                }
            }
            return new ResponseForRequest() { Successful = false, Errors = new List<string> { "Invalid datas, try again!" }};
        }

        private void UpdatePassword(AppUser appUser, string newPassword)
        {
            //bool result = _userManager.CheckPasswordAsync(appUser, newPassword).Result;
                var newHashPassword = _userManager.PasswordHasher.HashPassword(appUser, newPassword);
                appUser.PasswordHash = newHashPassword;
                var newUserUpdate = _userManager.UpdateAsync(appUser);
        }
    }
}