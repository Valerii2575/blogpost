using blogpost.Application.Common.Interfaces;
using MediatR;

namespace blogpost.Application.Command.Auth.Email
{
    public class ForgotUserNameOrPasswordHandler(IIdentityService identityService) : IRequestHandler<ForgotUserNameOrPasswordCommand, ForgotUserNameOrPasswordResult>
    {
        public async Task<ForgotUserNameOrPasswordResult> Handle(ForgotUserNameOrPasswordCommand cmd, CancellationToken cancellationToken)
        {
            var result = await identityService.ForgotUserNameOrPassword(cmd);
            return result;
        }
    }
}
