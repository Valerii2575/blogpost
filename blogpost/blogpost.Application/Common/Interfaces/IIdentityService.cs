using blogpost.Application.Command.Auth.Email;
using blogpost.Application.Command.Auth.Login;
using blogpost.Application.Command.Auth.Register;
using blogpost.Application.Command.Auth.ResetPasswor;
using blogpost.Application.DTOs;

namespace blogpost.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginCommandResult> Login(LoginCommand model);
        Task<RegisterCommandResult> Register(RegisterDto model);
        Task<ConfirmEmailResult> ConfirmEmail(ConfirmEmailCommand cmd);
        Task<ResendEmailConfirmationLinkResult> ResendEmailconfirmationLink(ResendEmailConfirmationLinkCommand cmd);
        Task<ForgotUserNameOrPasswordResult> ForgotUserNameOrPassword(ForgotUserNameOrPasswordCommand cmd);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordCommand cmd);
    }
}
