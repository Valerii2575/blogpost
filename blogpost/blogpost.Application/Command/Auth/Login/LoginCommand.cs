using blogpost.Application.DTOs;
using blogpost.Domain.Enums;
using MediatR;
using System;

namespace blogpost.Application.Command.Auth.Login
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        public LoginDto? LoginDto { get; set; }
    }

    public class LoginCommandResult
    {
        public UserDto? User { get; set; }
        public StatusResult Status { get; set; }
        public string? Message { get; set; }
    }
}
