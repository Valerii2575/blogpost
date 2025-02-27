using blogpost.Application.Command.Auth.Login;
using blogpost.Application.Command.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace blogpostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginCommandResult>> Login(LoginCommand cmd)
        {
            var user = await _sender.Send(cmd);

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterCommandResult>> Register(RegisterCommand cmd)
        {
            
            var result = await _sender.Send(cmd);

            return Ok(result);
        }

        

    }
}
