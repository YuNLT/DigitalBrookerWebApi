using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace DigitalBroker.Application.ServiceRegristrations
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
