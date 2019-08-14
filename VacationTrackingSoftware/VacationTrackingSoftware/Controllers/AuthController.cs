using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using VacationTrackingSoftware.Auth;
using VacationTrackingSoftware.Helpers;
using VacationTrackingSoftware.Token;
using VacationTrackingSoftware.ViewModels;

namespace VacationTrackingSoftware.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(
            UserManager<AppUser> userManager,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;         
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                logger.Info("user with username:" + credentials.UserName + " cant login");
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
            var user = _userManager.FindByNameAsync(credentials.UserName).Result;

            //if to be more roles for one user, than need change this code
            IList<string> currentRoles = await _userManager.GetRolesAsync(user);
            string currentRole = currentRoles.First();
            int RoleEnum = 0;
            if (currentRole == "Employee") RoleEnum = (int)Roles.Employee;
            if (currentRole == "HrUser") RoleEnum = (int)Roles.HrUser;
            if (currentRole == "Manager") RoleEnum = (int)Roles.Manager;
            if (currentRole == "Admin") RoleEnum = (int)Roles.Admin;

            //create claims for token 
            var claims = new List<Claim>();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, user, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented }, RoleEnum, claims);
            SendCookie(user);
            //var userId = User.FindFirst("id").Value;
            return new OkObjectResult(jwt);

        }


        //test
        private void SendCookie(AppUser user) {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("UserName", user.UserName, option);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            //check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }
            //Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}