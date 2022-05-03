﻿using System;
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

        public SecurityController(ISecurityService securityService)
        {
            this.securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
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
    }
}
