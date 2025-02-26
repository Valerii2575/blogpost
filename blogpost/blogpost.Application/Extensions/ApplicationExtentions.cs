using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace blogpost.Application.Extensions
{
    public static class ApplicationExtentions
    {
        public static IServiceCollection AddAplicationService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationExtentions).Assembly);
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
