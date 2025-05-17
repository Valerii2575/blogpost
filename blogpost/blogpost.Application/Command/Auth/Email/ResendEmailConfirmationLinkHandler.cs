using blogpost.Application.Common.Interfaces;
using MediatR;

namespace blogpost.Application.Command.Auth.Email
{
    public class ResendEmailConfirmationLinkHandler(IIdentityService service) : IRequestHandler<ResendEmailConfirmationLinkCommand, ResendEmailConfirmationLinkResult>
    {
        public async Task<ResendEmailConfirmationLinkResult> Handle(ResendEmailConfirmationLinkCommand cmd, CancellationToken cancellationToken)
        {
            var result = await service.ResendEmailconfirmationLink(cmd);

            return result;
        }
    }
}
