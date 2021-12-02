using System.Threading.Tasks;

namespace Mailing.Abstractions
{
    public interface IEmailSender
    {
        public Task SendEmail(string email, string subject, string message);
    }
}