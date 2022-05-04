using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaCantine.Security.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string message);
    }
}
