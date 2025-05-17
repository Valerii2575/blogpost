using blogpost.Application.DTOs;
using blogpost.Domain.Enums;
using MediatR;

namespace blogpost.Application.Command.Auth.ResetPasswor
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResult>
    {
        public ResetPassworDto ResetPassworDto { get; set; }

        public ResetPasswordCommand(ResetPassworDto resetPassworDto)
        {
            ResetPassworDto = resetPassworDto;
        }
    }

    public class ResetPasswordResult
    {
        public StatusResult Status { get; set; }
        public string? Message { get; set; }
    }
}
