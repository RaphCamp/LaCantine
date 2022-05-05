using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LaCantine.Security.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SampleJwtApp.Security.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private IEmailSender sender;

        public SecurityService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSender sender)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.sender = sender;
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName) != null;
        }
        
        public async Task<IdentityResult?> AddUserAsync(string name, string email, string phoneNumber, string password)
        {
            var appUser = new IdentityUser
            {
                UserName = name,
                Email = email,
                PhoneNumber = phoneNumber
            };

            return await userManager.CreateAsync(appUser, password);


            // TODO : Add other methods to manage roles
            //if (!await roleManager.RoleExistsAsync(userRegistration.UserRole))
            //{
            //    await roleManager.CreateAsync(new IdentityRole(userRegistration.UserRole));
            //}

            //if (await roleManager.RoleExistsAsync(userRegistration.UserRole))
            //{
            //    await userManager.AddToRoleAsync(appUser, userRegistration.UserRole);
            //}

        }

        public async Task<IdentityUser?> AuthenticateUserAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        
        public async Task<SecurityToken> BuildJwtTokenAsync(IdentityUser user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.Email, user.Email),
                    // JWT specific values, Iss and Aud must be set to the same values as the server's
                    // otherwise the token will not be valid when used against the api
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss, configuration["JsonWebTokenKeys:ValidIssuer"]),
                    new Claim(JwtRegisteredClaimNames.Aud, configuration["JsonWebTokenKeys:ValidAudience"]),
                };

            var userRoles = await userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JsonWebTokenKeys:SymmetricKey"]));

            var token = new JwtSecurityToken(
                // TODO : configurable token expiry ?
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public async Task<bool> SendResetPasswordEmailLink(string email)
        {
            var user = userManager.Users.First(u => u.Email == email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // TODO: enter the correct page url (this is your front-end page, not the API endpoint !)
            // TODO: you should get the base url from the configuration
            var url = $"http://localhost:5234/reset-password?username={user.UserName}&token={token}";
            return await sender.SendEmail(email, "Reset password", "Please confirm by clicking the following link.\r\n\r\n" + url);
        }



        public async Task<bool> ResetPassword(string userName, string token, string newPassword)
        {
            var user = userManager.Users.First(u => u.UserName == userName);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
    }
}
