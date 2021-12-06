
using AutoMapper;
using Hangfire;
using JuhinAPI.Data;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
using JuhinAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly IBackgroundJobClient backgroundJobs;

        public AccountsController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IMapper mapper,
            IEmailService emailService,
            IBackgroundJobClient backgroundJobs
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
            this.emailService = emailService;
            this.backgroundJobs = backgroundJobs;
        }
        /// <summary>
        /// Shows the list of all users (with pagination)
        /// </summary>
        /// <param name="paginationDTO">Sets the maximum records per page and the page numberr to show</param>
        /// <returns></returns>
        [HttpGet("Users", Name = "getUsers")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Users.AsQueryable();
            queryable = queryable.OrderBy(x => x.Email);

            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage);
            var users = await queryable.Paginate(paginationDTO).ToListAsync();

            return mapper.Map<List<UserDTO>>(users);
        }
        /// <summary>
        /// Shows all available roles in system
        /// </summary>
        /// <returns>string</returns>
        [HttpGet("Roles", Name = "getRoles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {
            return await context.Roles.Select(x => x.Name).ToListAsync();
        }
        /// <summary>
        /// Assigns the user Role in system (Admin, Specialist, ..)
        /// </summary>
        /// <param name="editRoleDTO"></param>
        /// <returns></returns>
        [HttpPost("AssignRole", Name = "assignRole")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> AssignRole(EditRoleDTO editRoleDTO)
        {
            var user = await userManager.FindByIdAsync(editRoleDTO.UserId);
            if (user == null)
            {
                return NotFound();
            }
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
            return NoContent();
        }
        /// <summary>
        /// Gets current user info
        /// </summary>
        /// <returns>string</returns>
        [HttpGet("userInfo", Name = "getUserInfo")]
        public async Task<ActionResult<CurrentUserInfo>> GetUserInfo()
        {
            var identityUser = await userManager.GetUserAsync(User);
            var user = new CurrentUserInfo
            {
                UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = HttpContext.User.FindFirstValue(ClaimTypes.Name),
                UserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role),
                EmailAddress = identityUser?.Email
            };
            //if (user.UserId == null && user.Name == null && user.UserRole == null) return NotFound();

            return Ok(user);
        }
        /// <summary>
        /// Removes the user Role in system (Roles: Admin)
        /// </summary>
        /// <param name="editRoleDTO"></param>
        /// <returns></returns>
        [HttpPost("RemoveRole", Name = "removeRole")]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> RemoveRole(EditRoleDTO editRoleDTO)
        {
            var user = await userManager.FindByIdAsync(editRoleDTO.UserId);
            if (user == null)
            {
                return NotFound();
            }
            await userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));

            return NoContent();
        }

        /// <summary>
        /// Create a user account with email address and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(UserToken), 200)]
        [HttpPost("Create", Name = "createAccount")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return await BuildToken(model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        /// <summary>
        /// Resets the password / for testing only / to be removed
        /// </summary>
        /// <param name="userResetInfo"></param>
        /// <returns></returns>

        //[ApiExplorerSettings(IgnoreApi = true)]
        //[HttpPost("{userName},{password}", Name = "resetPassword")]
        [HttpPost("ResetPassword", Name = "resetPassword")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> ResetPassword([FromBody] UserResetInfo userResetInfo)
        {
            var user = await userManager.FindByIdAsync(userResetInfo.UserId);
            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, userResetInfo.ResetPasswordToken, userResetInfo.Password);

                if (result.Succeeded)
                {
                    return Ok("Password changed succesfully.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return NotFound();
        }

        [HttpPost("ResetTokenRequest", Name = "resetTokenRequest")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> ResetTokenRequest([FromQuery] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                UserResetInfo userInfo = new UserResetInfo { UserId = user.Id, ResetPasswordToken = token};
                //var callbackUrl = Url.Action(action: "ResetPassword", controller:"Accounts", userInfo, protocol:Request.Scheme);
                //var callbackUrl = Url.Link(routeName: "resetPassword", new {Controller = "Accounts", Action = "ResetPassword", code = token });
                var callbackUrl = Url.Action(action: null,  controller:"Accounts", userInfo, protocol: Request.Scheme);
                SendLinkByEmail(callbackUrl, user);
                return Ok("Link was send via Email");
            }
            return NotFound();
        }

        private async Task<UserToken> BuildToken(UserInfo userInfo)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userInfo.EmailAddress),
                    new Claim(ClaimTypes.Email, userInfo.EmailAddress),
                    new Claim("mykey", "whatever value I want")
                };

                var identityUser = await userManager.FindByEmailAsync(userInfo.EmailAddress);
                var claimsDB = await userManager.GetClaimsAsync(identityUser);

                claims.AddRange(claimsDB);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(7);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds
                    );
                return new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            }

        /// <summary>
        /// Login action (returns JwtBearer token)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login", Name = "loginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {
            var result = await signInManager.PasswordSignInAsync(model.EmailAddress, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await BuildToken(model);
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }
        /// <summary>
        /// Logout current user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout", Name = "loginOutUser")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            var result = signInManager.SignOutAsync().IsCompletedSuccessfully;
            if (result)
            {
                return Ok("Logged out successfully");
            }
            return BadRequest("Invalid logout attempt");
        }
        /// <summary>
        /// Action to renew the token (to be used at client side)
        /// </summary>
        /// <returns></returns>
        [HttpPost("RenewToken", Name = "renewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> Renew()
        {
            var userInfo = new UserInfo
            {
                EmailAddress = HttpContext.User.Identity.Name
            };
            return await BuildToken(userInfo);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendLinkByEmail(string url, IdentityUser user)
        {
            

                var message = new EmailMessage();
                message.Subject = "JuhinAPI Password Reset Notification";
                message.Content =
                    "<div style=\"margin: 10px; width:400px; font-family: Calibri; font-size:16px; border-radius:16px; overflow:hidden; background-image: linear-gradient(#161f24, #727272, #969696); background-color: #95bebe;\">" +
                        "<div style=\"border-radius:0px; text-align:center; padding:2px; margin:0px; color:white; font-size:20px;background-image: linear-gradient(#00cfeb,#008fa8,#006f88, #006f88, #005f78); background-color:#95bebe;\">" +
                            "<p style=\"margin-bottom: 2px;\"> PASSWORD RESET NOTIFICATION</p>" +
                            "<p style=\"font-size:16px; margin-top: 2px;\">To reset your password, please use the link below:</p>" +
                        "</div>" +
                        "<div style=\"padding:5px 10px 5px 10px; color:#e3e3e3; background-color:#c7f1fa;\">" +
                            "<p style=\"text-align:center; color:#e3e3e3\"><a href = "+url+" > RESET PASSWORD LINK </a></p>" +
                        "</div>" +
                    "</div>";
                message.FromAddress.Name = "JuhinAPI Software";
                message.FromAddress.Address = "pipsitestemail@gmail.com";
                            
                    message.ToAddresses.Add(new EmailAddress { Name = "Dear " + user.UserName.ToLower(), Address = user.Email });
                
                try
                {
                    backgroundJobs.Enqueue(() => emailService.Send(message));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    var errorMessage = new EmailMessage();
                    errorMessage.Content = "<p>ERROR MESSAGE: " + ex.Message + " / " + ex.TargetSite.Name.ToString() + "</p>";
                    errorMessage.Subject = "JuhinAPI ERROR/EXCEPTION Notification";
                    errorMessage.FromAddress.Name = "JuhinAPI Software";
                    errorMessage.ToAddresses.Add(new EmailAddress { Name = "Hermano", Address = "lkuczma@gmail.com" });
                    errorMessage.FromAddress.Address = "pipsitestemail@gmail.com";
                    emailService.Send(errorMessage);
                }
            

        }

    }
}
