using blogpost.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace blogpost.Infrastructure.Extentions
{
    public static class InfrastructureExtentions
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            
            //services.AddScoped<UserManager<UserDto>>();
            //services.AddScoped<SignInManager<UserDto>>();

            return services;
        }
    }
}
