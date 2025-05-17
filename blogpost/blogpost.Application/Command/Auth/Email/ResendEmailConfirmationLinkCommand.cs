using blogpost.Domain.Enums;
using MediatR;

namespace blogpost.Application.Command.Auth.Email
{
    public class ResendEmailConfirmationLinkCommand : IRequest<ResendEmailConfirmationLinkResult>
    {
        public string? Email { get; set; }

        public ResendEmailConfirmationLinkCommand(string email)
        {
            Email =  email;
        }

        public ResendEmailConfirmationLinkCommand() { }
    }

    public class ResendEmailConfirmationLinkResult
    {
        public StatusResult Status { get; set; }
        public string? Message { get; set; }
    }
}
