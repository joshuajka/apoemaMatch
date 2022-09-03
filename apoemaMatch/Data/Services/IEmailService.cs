using apoemaMatch.Data.ViewModels;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);

        Task SendForConfirmationRegistrationEmail(UserEmailOptions userEmailOptions);
    }
}
