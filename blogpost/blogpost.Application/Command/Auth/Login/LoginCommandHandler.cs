using blogpost.Application.Common.Interfaces;
using MediatR;

namespace blogpost.Application.Command.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IIdentityService _identityService;
        public LoginCommandHandler(IIdentityService identityService) 
        {
            _identityService = identityService;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _identityService.Login(command);

            return result;
        }
    }
}
