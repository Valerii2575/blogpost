using blogpost.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogpost.Application.Command.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService) 
        {
            _identityService = identityService;
        }

        public async Task<RegisterCommandResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _identityService.Register(command.RegisterDto);

            return result;
        }
    }
}
