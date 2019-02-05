using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VacationTrackingSoftware.Auth;
using VacationTrackingSoftware.Token;

namespace VacationTrackingSoftware.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, AppUser user, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings, int roles,  List<Claim> claims)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(user, identity, claims),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
                rolesUser = roles,
                name=user.UserName,
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
