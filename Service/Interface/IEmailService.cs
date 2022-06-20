using Shared.Models;

namespace Service.Interface
{
    public interface IEmailService
    {
        void SendCode(EmailConfigurationModel _emailConfig);
    }
}
