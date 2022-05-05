using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace SampleJwtApp.Security.Services
{
    public interface ISecurityService
    {
        Task<bool> UserExistsAsync(string userName);
        Task<IdentityResult?> AddUserAsync(string name, string email, string phoneNumber, string password);
        Task<IdentityUser?> AuthenticateUserAsync(string username, string password);
        Task<SecurityToken> BuildJwtTokenAsync(IdentityUser user);

        Task<bool> SendResetPasswordEmailLink(string email);
        Task<bool> ResetPassword(string userName, string token, string newPassword);
    }
}