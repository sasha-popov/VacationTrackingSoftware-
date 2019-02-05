using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VacationTrackingSoftware.Token;

namespace VacationTrackingSoftware.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(AppUser user, ClaimsIdentity identity, List<Claim> claims)
        {
            //   var claims = new List<Claim>
            //{
            //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
            //        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            //        identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
            //        identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
            //    };
            //   claims.AddRange(userClaims);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));
            claims.Add(identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol));
            claims.Add(identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id));


            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
