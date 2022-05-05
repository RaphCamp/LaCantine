using LaCantine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Security.Services
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string email, string subject, string message);
    }
}
