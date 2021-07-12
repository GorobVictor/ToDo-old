using Core.Dto.UserDto;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IUserService userSvc { get; set; }
        IMyAuthorizationServiceSingelton myAuthorizationSvc { get; set; }

        public AccountController(
            IUserService userSvc,
            IMyAuthorizationServiceSingelton myAuthorizationSvc
            )
        {
            this.userSvc = userSvc;
            this.myAuthorizationSvc = myAuthorizationSvc;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserAuth user)
        {
            return Ok(await GetIdentityAsync(user));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUp user)
        {
            var result = await this.userSvc.SignUp(user);

            if (result != null)
                return Ok(await GetIdentityAsync(new UserAuth(user.Email, user.Password)));

            return BadRequest();
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok(this.userSvc.GetUserByUserId(this.myAuthorizationSvc.UserIdAuthenticated, includeTasks: true));
        }

        private async Task<GetTokenResult> GetIdentityAsync(UserAuth user)
        {
            var User = userSvc.GetUserByLoginAndPassword(user, includeTasks: true);

            if (User != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, User.FullName),
                    new Claim("userId", User.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                if (claimsIdentity == null)
                {
                    throw new Exception("Invalid username or password.");
                }

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new GetTokenResult(encodedJwt, User);
            }

            return null;
        }
    }
}
