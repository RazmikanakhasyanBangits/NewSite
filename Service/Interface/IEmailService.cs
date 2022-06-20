using Shared.Models;

namespace Service.Interface
{
    public interface IEmailService
    {
        Task SendCode(EmailConfigurationModel _emailConfig);
    }
}
