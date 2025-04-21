using DigitalBroker.Application.Abstracts;
using DigitalBroker.Application.Commands;
using DigitalBroker.Application.Services;
using DigitalBrooker.Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DigitalBroker.Application.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISmtpEmailService, SmtpEmailService>();

            //MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));

            //smtp
            // Bind and register SmtpInfo as a singleton
            var smtpSettings = new SmtpInfo();
            configuration.GetSection("SmtpInfo").Bind(smtpSettings);
            services.AddSingleton(smtpSettings);

            return services;
        }
    }
}
