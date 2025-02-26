using blogpost.Application.DTOs;

namespace blogpost.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<UserDto> Login(LoginDto model);
        Task<bool> Register(RegisterDto model);
    }
}
