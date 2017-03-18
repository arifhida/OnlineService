using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineService.API.Models;
using OnlineService.API.Options;
using OnlineService.Data.Abstracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OnlineService.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;
        public TokenController(IOptions<JwtIssuerOptions> jwtOptions, IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _jwtOptions = jwtOptions.Value;
            _userRepository = userRepository;

            _roleRepository = roleRepository;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody] ApplicationUser applicationUser)
        {
            var userIdentity = await GetClaimIdentity(applicationUser);
            if (userIdentity == null)
            {
                return BadRequest("invalid credential");
            }
            var roles = await _userRepository.GetSingleAsync(x => x.UserName == applicationUser.UserName && x.Password == applicationUser.Password,
                y => y.UserRole);
            var roleClaims = new List<Claim>();
            foreach (var item in roles.UserRole)
            {
                var role = _roleRepository.GetSingle(item.RoleId);
                roleClaims.Add(new Claim("Roles", role.RoleName));
            }
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                    userIdentity.FindFirst("Username")
                };

            var claimlist = claims.ToList();
            claimlist.AddRange(roleClaims);


            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claimlist.AsEnumerable(),
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalDays
            };
            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaimIdentity(ApplicationUser appUser)
        {
            var result = _userRepository.GetSingle(x => x.UserName == appUser.UserName && x.Password == appUser.Password);
            if (result != null)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(appUser.UserName, "Token"),
                    new[]
                    {
                        new Claim("Username",appUser.UserName)

                    }
                    );

                return Task.FromResult(identity);
            }
            return Task.FromResult<ClaimsIdentity>(null);
        }


        //public async Task<IActionResult> Post()
        //{

        //}
    }
}
