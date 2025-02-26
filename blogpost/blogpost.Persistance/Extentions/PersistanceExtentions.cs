using blogpost.Domain.Entities;
using blogpostApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace blogpost.Persistance.Extentions
{
    public static class PersistanceExtentions
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogPostAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        /// <summary>
        /// Defining our IdentityService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            //services.AddIdentityCore<User>(options =>
            //{
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = true;

            //    options.SignIn.RequireConfirmedEmail = true;
            //})
            //    .AddRoles<IdentityRole>()
            //    .AddRoleManager<RoleManager<IdentityRole>>()
            //    .AddEntityFrameworkStores<BlogPostAppDbContext>()
            //    .AddSignInManager<SignInManager<User>>()
            //    .AddUserManager<UserManager<User>>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),
            //            ValidIssuer = config["JWT:Issuer"],
            //            ValidateIssuer = true,
            //            ValidateAudience = false
            //        };
            //    });

            return services;
        }
    }
}
