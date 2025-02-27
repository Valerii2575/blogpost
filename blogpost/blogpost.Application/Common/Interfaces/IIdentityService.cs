using blogpost.Application.Command.Auth.Login;
using blogpost.Application.Command.Auth.Register;
using blogpost.Application.DTOs;

namespace blogpost.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginCommandResult> Login(LoginCommand model);
        Task<RegisterCommandResult> Register(RegisterDto model);
    }
}
