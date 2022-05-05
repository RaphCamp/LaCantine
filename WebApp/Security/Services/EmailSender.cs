using LaCantine.Data;
using LaCantine.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace LaCantine.Security.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            Utilisateur user = new Utilisateur(); 
            
        }
        public async Task SendEmail(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }



        public async Task ResetPassword(Utilisateur user, string sBaseUrl, string token )
        {
            
            var callbackUrl = string.Format("{0}#!/set-password?id={1}&token={2}", sBaseUrl, user.Id, HttpUtility.UrlEncode(token));
            var subject = "Réinitialiser le mot de passe ";
            var body = string.Format(@"Réinitialiser le mot de passe en cliquant ici : <a href=""{0}"">{0}</a>", callbackUrl);
            await this.SendEmail(user.Mail, subject, body);

        }
    }
}
