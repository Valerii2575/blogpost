using blogpost.Application.Common.Interfaces;
using blogpost.Application.DTOs;
using blogpost.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogpostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;        

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var result = await _identityService.Login(model);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            
            var result = await _identityService.Register(model);

            return Ok("Your account has been created, you can ");
        }

        

    }
}
