using blogpost.Domain.Enums;
using MediatR;

namespace blogpost.Application.Command.Auth.Email
{
    public class ForgotUserNameOrPasswordCommand : IRequest<ForgotUserNameOrPasswordResult>
    {
        public string? Email { get; set; }

        public ForgotUserNameOrPasswordCommand(string email)
        {
            Email = email;
        }

        public ForgotUserNameOrPasswordCommand() { }
    }

    public class ForgotUserNameOrPasswordResult
    {
        public StatusResult Status { get; set; }
        public string? Message { get; set; }
    }
}
