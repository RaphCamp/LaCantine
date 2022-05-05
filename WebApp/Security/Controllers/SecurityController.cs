using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleJwtApp.Common;
using SampleJwtApp.Security.Services;
using SampleJwtApp.Security.ViewModels;
using LaCantine.Security.Services;
using System.Web.Http;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using LaCantine.Model;
using System.Web;

namespace SampleJwtApp.Security.Controllers
{
    /// <summary>
    /// Provides security endpoints for the application.
    /// <p>Through this API, users will be able to login, change their password, register etc.</p>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService securityService;
        private readonly IEmailSender emailSender;
        private UserManager<Utilisateur> _userManager;

        public SecurityController(ISecurityService securityService, IEmailSender emailSender)
        {
            this.securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
            this.emailSender = emailSender;
        }

        /// <summary>
        /// Register as a new user of the service
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns>
        /// <p>200 OK if the user registration information was correct</p>
        /// <p>400 Bad Request if the user registration information was incorrect (password policy issue, duplicate user name or email etc.</p>
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistration userRegistration)
        {
            // TODO : more validation is required
            if (string.IsNullOrEmpty(userRegistration?.Password) || string.IsNullOrEmpty(userRegistration?.Name) || string.IsNullOrEmpty(userRegistration?.Email))
            {
                return BadRequest(new Response { Status = "Error", Message = "Missing data" });
            }

            // TODO: decide if we want to give this information or not, potential security concerns ?
            if (await securityService.UserExistsAsync(userRegistration.Name))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            var result = await securityService.AddUserAsync(
                userRegistration.Name,
                userRegistration.Email,
                userRegistration.PhoneNo ?? "",
                userRegistration.Password);

            if (result?.Succeeded == false)
            {
                // TODO: better messages for the obvious errors (password policy etc.)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        /// <summary>
        /// Tries to authenticate using the credentials supplied.
        /// </summary>
        /// <param name="credentials">The credentials, containing the user name and password</param>
        /// <returns>200 OK with a valid JWT token usable on the other authenticated endpoints,
        /// or 401 if credentials do not match a valid user</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            // TODO : more validation is required
            if (string.IsNullOrEmpty(credentials?.Password) || string.IsNullOrEmpty(credentials?.UserName))
            {
                return BadRequest(new Response { Status = "Error", Message = "Missing credentials" });
            }

            var user = await securityService.AuthenticateUserAsync(credentials.UserName, credentials.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = await securityService.BuildJwtTokenAsync(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                userName = credentials.UserName,
                status = "Login successful, token issued, send it back in a Bearer header to authenticate subsequent requests"
            });
        }

        //envoyer mail oubli mdp
        [AllowAnonymous]
        [HttpPost]
        [Route("account/send-email")]
        public async Task<IActionResult> SendEmailAsync([FromUri] string email, string subject, string message)
        {
            await emailSender.SendEmail(email, subject, message);
            return Ok();
        }

        //reset password

        public async Task ResetPassword(Utilisateur user, string sBaseUrl)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = string.Format("{0}#!/set-password?id={1}&token={2}", sBaseUrl, user.Id, HttpUtility.UrlEncode(token));
            var subject = "Réinitialiser le mot de passe ";
            var body = string.Format(@"Réinitialiser le mot de passe en cliquant ici : <a href=""{0}"">{0}</a>", callbackUrl);
            await emailSender.SendEmail(user.Mail, subject, body);

        }




        //changer mdp
    }
}
