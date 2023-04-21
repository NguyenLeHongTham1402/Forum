using Forum.BLL;
using Forum.Common.Req;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserSvc userSvc = new UserSvc();
        private readonly JWTConfig _jWTConfig;

        public UserController(IOptions<JWTConfig> jWTConfig)
        {
            _jWTConfig = jWTConfig.Value;
        }

        [HttpPost("signup")]
        public IActionResult SignupUser([FromBody] _UserReq userReq)
        {
            var res = userSvc.CreateUser(userReq);
            return Ok(res);
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] _UserReq userReq)
        {
            var res = userSvc.SignIn(userReq);
            if (res.Success)
            {
                var u = (_UserReq)res.Data;
                u.Token = GenerateToken(u);
            }
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpGet("list-users")]
        public IActionResult GetListUsers()
        {
            return Ok(userSvc.GetListUsers());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpPost("list-users/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            return Ok(userSvc.GetUserByUsername(username));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpPatch("update/{username}")]
        public IActionResult UpdateUser(_UserReq userReq, string username)
        {
            var res = userSvc.UpdateUser(userReq, username);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpDelete("delete/{username}")]
        public IActionResult DeleteUser(string username)
        {
            return Ok(userSvc.DeleteUser(username));
        }

        private string GenerateToken(_UserReq user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.key);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("Password", user.Password),
                    new Claim("Avatar", user.Avatar),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.audience,
                Issuer = _jWTConfig.issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
