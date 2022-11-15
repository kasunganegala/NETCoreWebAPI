
//using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using System.Security.Cryptography;
using DataAccess.Models.Authentication;
using DataAccess.Models;

namespace NETCoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserData _userData;
        private static MailMessage _global = new MailMessage();

        public AuthenticationController(
            IConfiguration configuration,
            IUserData userData)
        {
            _configuration = configuration;
            _userData = userData;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {

            try
            {
                return Ok(await _userData.GetUsers());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("Users/{id}")]
        public async Task<ActionResult<UserData>> GetUser(int id)
        {
            try
            {
                return Ok(await _userData.GetUser(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return Ok(new
                    {
                        title = "Error",
                        errors = "Email address and password required",
                        status = 401
                    });
                }

                var passwordHash = "";
                using (var sha256 = SHA256.Create())
                {
                    var varhashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
                    passwordHash = BitConverter.ToString(varhashedBytes).Replace("-", "").ToLower();
                }

                UserModel user = await _userData.GetUser(model.Email, passwordHash);

                if (user is null)
                {
                    return Ok(new
                    {
                        title = "Unauthorized",
                        errors = "Invalid email and/or password",
                        status = 401
                    });
                }
                else if (user.IsDeactivated)
                {
                    return Ok(new
                    {
                        title = "Unauthorized",
                        errors = "Account Deactivated",
                        status = 401
                    });
                }

                IEnumerable<RolesModel> roles = await _userData.GetUserRoles(user.Id);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (RolesModel userRole in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
                }

                var authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256Signature)
                    );

                return Ok(new
                {
                    errors = "",
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo.ToString("yyyy-MM-ddThh:mm:ss"),
                    title = "Authorized",
                    status = 200,
                    roles = roles.ToList().Select(s => s.RoleName),
                    name = string.Concat(user.FirstName, " ", user.LastName),
                    email = user.Email,
                    username = user.UserName,
                    customerId = user.CustomerId,
                    contractorId = user.ContractorId
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
