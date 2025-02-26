using blogpost.Domain.Entities;
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

        public async Task<UserDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                //return Unauthorized("Invalid user email or password");
            }

            if (!user.EmailConfirmed)
            {

            }
            //return Unauthorized("Please confirmed your email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                //return Unauthorized("Invalid user email or password");
            }

            //user.JWT = CreateJwt(user);
            return new UserDto();
        }

        public async Task<bool> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                //return BadRequest($"An existing account is using {model.Email}, email address.");
            }

            var userToAdd = new UserDto
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true
            };

            //var result = await _userManager.CreateAsync(userToAdd, model.Password);
            //if (!result.Succeeded)
            //{
            //    // return BadRequest(result.Errors);
            //}

            return true; // result.Succeeded;
        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(e => e.Email == email.ToLower());
        }

        private string CreateJwt(UserDto userDto)
        {
            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new(ClaimTypes.Email, userDto.Email),
                new(ClaimTypes.GivenName, userDto.FirstName),
                new(ClaimTypes.GivenName, userDto.LastName),
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
