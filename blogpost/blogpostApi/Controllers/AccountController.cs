using blogpost.Application.Command.Auth.Email;
using blogpost.Application.Command.Auth.Login;
using blogpost.Application.Command.Auth.Register;
using blogpost.Application.Command.Auth.ResetPasswor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPut("confirm-email")]
        public async Task<ActionResult<ConfirmEmailResult>> ConfirmEmail(ConfirmEmailCommand cmd)
        {
            var result = await _sender.Send(cmd);
            return Ok(result);
        }

        [HttpPost("resend-email-confirmation-link/{email}")]
        public async Task<ActionResult<ResendEmailConfirmationLinkResult>> ResendEmailconfirmationLink(string email)
        {
            var result = await _sender.Send(new ResendEmailConfirmationLinkCommand(email));

            return Ok(result);
        }

        [HttpPost("forgot-username-or-password/{email}")]
        public async Task<ActionResult<ForgotUserNameOrPasswordResult>> ForgotUserNameOrPassword(ForgotUserNameOrPasswordCommand cmd)
        {
            var result = await _sender.Send(cmd);
            return Ok(result);
        }

        [HttpPut("reset-password")]
        public async Task<ActionResult<ResetPasswordResult>> ResetPassword(ResetPasswordCommand cmd)
        {
            var result = await _sender.Send(cmd);
            return Ok(result);
        }

        //[Authorize]
        //[HttpGet("refresh-user-token")]
        //public async Task<ActionResult<>>

    }
}
