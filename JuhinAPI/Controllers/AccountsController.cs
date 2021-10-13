
using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        public AccountsController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        /// Removes the user Role in system (Roles: Admin)
        /// </summary>
        /// <param name="editRoleDTO"></param>
        /// <returns></returns>
        [HttpPost("RemoveRole", Name = "removeRole")]
        [ProducesResponseType(404)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
                return await BuildToken (model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        /// <summary>
        /// Resets the password / for testing only / to be removed
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("{userName},{password}", Name = "resetPassword")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> ResetPassword(string userName, string password)
        {
            //UserInfo model
            //var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, password);

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



    }
}
