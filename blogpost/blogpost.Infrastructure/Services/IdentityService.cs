using blogpost.Application.Command.Auth.Login;
using blogpost.Application.Command.Auth.Register;
using blogpost.Application.Mapping.UserAccount;
using blogpost.Domain.Entities;
using blogpost.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace blogpost.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _jwtKey;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IdentityService(IConfiguration config, UserManager<User> userManager,
                SignInManager<User> signInManager) 
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }

        public async Task<LoginCommandResult> Login(LoginCommand cmd)
        {
            var user = await _userManager.FindByEmailAsync(cmd.LoginDto.Email);
            if (user == null)
            {
                return new LoginCommandResult
                {
                    Message = "Invalid user email or password",
                    Status = StatusResult.BadRequest,
                    User = null
                };
            }

            if (!user.EmailConfirmed)
            {
                return new LoginCommandResult
                {
                    Message = "Please confirmed your email",
                    Status = StatusResult.ConfirmedEmail,
                    User = null
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, cmd.LoginDto.Password, false);
            if (!result.Succeeded)
            {
                return new LoginCommandResult
                {
                    Message = "Invalid user email or password",
                    Status = StatusResult.BadRequest,
                    User = null
                };
            }
            var userDto = user.ToData();
            userDto.JWT = CreateJwt(userDto);

            return new LoginCommandResult
            {
                Message = "",
                Status = StatusResult.Success,
                User = userDto
            };
        }

        public async Task<RegisterCommandResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                return new RegisterCommandResult 
                { 
                    Status = StatusResult.Success,
                    Message = $"An existing account is using {model.Email}, email address."
                };
            }

            var userToAdd = new User
            {
                UserName = model.FirstName,
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow                
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded)
            {
               return new RegisterCommandResult { Status = StatusResult.BadRequest, 
                            Message = string.Join(';', result.Errors.Select(x => x.Description).ToList()) };
            }

            return new RegisterCommandResult { Status = StatusResult.Success };
        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(e => e.Email == email.ToLower());
        }

        private string CreateJwt(UserDto userDto)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.GivenName, userDto.FirstName),
                new Claim(ClaimTypes.Surname, userDto.LastName),
            };

            var creadentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);
            var tockenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                SigningCredentials = creadentials,
                Issuer = _config["JWT:Issuer"]
            };

            var tockenHandler = new JwtSecurityTokenHandler();
            var jwt = tockenHandler.CreateToken(tockenDescriptior);

            return tockenHandler.WriteToken(jwt);
        }
    }
}
