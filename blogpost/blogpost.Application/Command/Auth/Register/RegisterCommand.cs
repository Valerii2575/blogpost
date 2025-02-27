using blogpost.Application.DTOs;
using blogpost.Domain.Enums;
using MediatR;

namespace blogpost.Application.Command.Auth.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResult>
    {
        public RegisterDto? RegisterDto { get; set; }
    }

    public class RegisterCommandResult
    {
        public StatusResult Status { get; set; }
        public string? Message { get; set; }
    }
}
