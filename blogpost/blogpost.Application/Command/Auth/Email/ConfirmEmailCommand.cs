using blogpost.Application.DTOs;
using blogpost.Domain.Enums;

namespace blogpost.Application.Command.Auth.Email
{
    public class ConfirmEmailCommand : IRequest<ConfirmEmailResult>
    {
        public ConfirmedEmailDto ConfirmedEmail { get; set; }
    }

    public class ConfirmEmailResult
    {
        public StatusResult Status { get; set; }
        public string? Message { get; set; }
    }
}
