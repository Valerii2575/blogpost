using blogpost.Application.Common.Interfaces;
using MediatR;

namespace blogpost.Application.Command.Auth.ResetPasswor
{
    public class ResetPasswordHandler(IIdentityService identityService) : IRequestHandler<ResetPasswordCommand, ResetPasswordResult>
    {
        public async Task<ResetPasswordResult> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await identityService.ResetPassword(command);

            return result;
        }
    }
}
