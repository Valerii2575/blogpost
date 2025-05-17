using blogpost.Application.Command.Auth.Email;
using blogpost.Application.Command.Auth.Login;
using blogpost.Application.Command.Auth.Register;
using blogpost.Application.Command.Auth.ResetPasswor;
using blogpost.Application.Mapping.UserAccount;
using blogpost.Domain.Entities;
using blogpost.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly IEmailService _emailService;

        public IdentityService(IConfiguration config, UserManager<User> userManager,
                IEmailService emailService,
                SignInManager<User> signInManager) 
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

        //public async Task<GetRefreshUserTokenQueryResult> RefreshUserToken()
        //{
        //    var nameIdentifier = ClaimTypes.NameIdentifier;
        //    var user = await _userManager.FindByIdAsync(nameIdentifier);


        //}

        public async Task<RegisterCommandResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                return new RegisterCommandResult 
                { 
                    Status = StatusResult.BadRequest,
                    Message = $"An existing account is using {model.Email}, email address."
                };
            }

            var userToAdd = new User
            {
                UserName = model.FirstName,
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                Email = model.Email.ToLower(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow                
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded)
            {
               return new RegisterCommandResult { Status = StatusResult.BadRequest, 
                            Message = string.Join(';', result.Errors.Select(x => x.Description).ToList()) };
            }
            else
            {
                if(await SendConfirmEmailAsync(userToAdd))
                return new RegisterCommandResult { Status = StatusResult.Success, Message = $"Your account {model.FirstName}, has been created, you can login" };
            }

            return new RegisterCommandResult { };
        }

        public async Task<ConfirmEmailResult> ConfirmEmail(ConfirmEmailCommand cmd)
        {
            var user = await _userManager.FindByEmailAsync(cmd.ConfirmedEmail.Email);
            if (user == null)
            {
                return new ConfirmEmailResult
                {
                    Status = StatusResult.Unauthorized,
                    Message = "This email addres has not registered yet"
                };
            }

            if (user.EmailConfirmed)
            {
                return new ConfirmEmailResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Your email was confirmed before. Please login to your account"
                };
            }

            try
            {
                var decodedTockenBytes = WebEncoders.Base64UrlDecode(cmd.ConfirmedEmail.Token);
                var decodedTocken = Encoding.UTF8.GetString(decodedTockenBytes);

                var result = await _userManager.ConfirmEmailAsync(user, decodedTocken);
                if (!result.Succeeded)
                {
                    return new ConfirmEmailResult
                    {
                        Status = StatusResult.InternalError,
                        Message = "Somthing wrong. Try to do leter"
                    };
                };
            }
            catch (Exception ex) 
            {
                return new ConfirmEmailResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try to do leter"
                };
            }

            return new ConfirmEmailResult 
                    {
                        Status = StatusResult.Success,
                        Message = "Your email address is confirmed. You can login now"
                    };
        }

        public async Task<ResendEmailConfirmationLinkResult> ResendEmailconfirmationLink(ResendEmailConfirmationLinkCommand cmd)
        {
            if(string.IsNullOrEmpty(cmd.Email))
            {
                return new ResendEmailConfirmationLinkResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Invalid email"
                };
            }

            var user = await _userManager.FindByEmailAsync(cmd.Email);
            if (user == null) 
            {
                return new ResendEmailConfirmationLinkResult
                {
                    Status = StatusResult.Unauthorized,
                    Message = "This email addres has not been registered yet"
                };
            }
            if (user.EmailConfirmed)
            {
                return new ResendEmailConfirmationLinkResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Your email address was confirmed befor. Pleas login to your account"
                };
            }

            try
            {
                if(await SendConfirmEmailAsync(user))
                {
                    return new ResendEmailConfirmationLinkResult
                    {
                        Status = StatusResult.Success,
                        Message = "Confirmation link sent. Please confirm your email address"
                    };
                }

                return new ResendEmailConfirmationLinkResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try it leter"
                };
            }
            catch (Exception ex)
            {
                return new ResendEmailConfirmationLinkResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try it leter"
                };
            }
        }

        public async Task<ForgotUserNameOrPasswordResult> ForgotUserNameOrPassword(ForgotUserNameOrPasswordCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.Email))
            {
                return new ForgotUserNameOrPasswordResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Invalid email"
                };
            }

            var user = await _userManager.FindByEmailAsync(cmd.Email);
            if (user == null)
            {
                return new ForgotUserNameOrPasswordResult
                {
                    Status = StatusResult.Unauthorized,
                    Message = "This email addres has not been registered yet"
                };
            }
            if (user.EmailConfirmed)
            {
                return new ForgotUserNameOrPasswordResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Your email address was confirmed befor. Pleas login to your account"
                };
            }

            try
            {
                if(await SendForgotUserNameOrPassword(user))
                {
                    return new ForgotUserNameOrPasswordResult
                    {
                        Status = StatusResult.Success,
                        Message = "Forgot username or password sent. Please check your email"
                    };
                }

                return new ForgotUserNameOrPasswordResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try it leter"
                };
            }
            catch (Exception ex)
            {
                return new ForgotUserNameOrPasswordResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try it leter"
                };
            }
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.ResetPassworDto.Email))
            {
                return new ResetPasswordResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Invalid email"
                };
            }

            var user = await _userManager.FindByEmailAsync(cmd.ResetPassworDto.Email);
            if (user == null)
            {
                return new ResetPasswordResult
                {
                    Status = StatusResult.Unauthorized,
                    Message = "This email addres has not been registered yet"
                };
            }
            if (user.EmailConfirmed)
            {
                return new ResetPasswordResult
                {
                    Status = StatusResult.BadRequest,
                    Message = "Your email address was confirmed befor. Pleas login to your account"
                };
            }

            try
            {
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(cmd.ResetPassworDto.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

                var result = await _userManager.ResetPasswordAsync(user, decodedToken, cmd.ResetPassworDto.NewPassword);
                if (result.Succeeded)
                {
                    return new ResetPasswordResult
                    {
                        Status = StatusResult.Success,
                        Message = "Pussword has been reset"
                    };
                }
                return new ResetPasswordResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try it leter"
                };
            }
            catch (Exception ex) 
            {
                return new ResetPasswordResult
                {
                    Status = StatusResult.InternalError,
                    Message = "Somthing wrong. Try it leter"
                };
            }
        }

        //private async Task<UserDto> CreateApplicationUserDto(User user)
        //{
        //    await SaveRefreshTokenAsync(user)
        //}

        //private async Task SaveRefreshTokenAsync(User user)
        //{
        //    var refreshToken = _jw
        //}

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(e => e.Email == email.ToLower());
        }

        private async Task<bool> SendConfirmEmailAsync(User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ConfirmEmailPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                        "<p>Please confirm your email address</p>" +
                        $"<p><a href=\"{url}\"</p>" +
                        $"<br>{_config["Email:ApplocationName"]}";

            var emailSend = new EmailSendDto(user.Email, "admin@gmail.com", "Confirm your email", body);

            return await _emailService.SendEmailAsync(emailSend);
        }

        private async Task<bool> SendForgotUserNameOrPassword(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var url = $"{_config["JWT:ClientUrl"]}/{_config["Email:ResetPasswordPath"]}?token={token}&email={user.Email}";

            var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
                        $"<p>Username: {user.UserName}</p>" +
                        "<p>In order to reset your password, please click on the following link.</p>" +
                        $"<p><a href=\"{url}\"</p>" +
                        $"<br>{_config["Email:ApplocationName"]}";

            var emailSend = new EmailSendDto(user.Email, "admin@gmail.com", "Forgot your username or password", body);

            return await _emailService.SendEmailAsync(emailSend);
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
