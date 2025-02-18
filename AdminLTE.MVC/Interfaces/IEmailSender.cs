using System.Threading.Tasks;

namespace AdminLTE.MVC
{
    public interface IEmailSender { Task SendEmailAsync(string email, string subject, string htmlMessage); }
}
