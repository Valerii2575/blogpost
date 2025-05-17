using blogpost.Application.Command.Auth.Email;
using blogpost.Application.DTOs;

namespace blogpost.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailSendDto emailSend);
        
    }
}
