using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VacationTrackingSoftware.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(AppUser user, ClaimsIdentity identity, List<Claim> claims);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
