using System.Threading.Tasks;

namespace Musicorum.Services
{
    public interface IEmailSender : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}