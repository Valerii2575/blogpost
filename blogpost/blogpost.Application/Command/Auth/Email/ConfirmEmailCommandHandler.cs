using blogpost.Application.Common.Interfaces;

namespace blogpost.Application.Command.Auth.Email
{
    public class ConfirmEmailCommandHandler(IIdentityService identityService) : IRequestHandler<ConfirmEmailCommand, ConfirmEmailResult>
    {

        public async Task<ConfirmEmailResult> Handle(ConfirmEmailCommand cmd, CancellationToken cancellationToken)
        {
            var result = await identityService.ConfirmEmail(cmd);
            return result;
        }
    }
}
