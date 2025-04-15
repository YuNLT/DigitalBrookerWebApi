using DigitalBrookerWebApi.Contract;

namespace DigitalBrookerWebApi.Extensions
{
    public static class EndpointExtensions
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            var endpoints = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpoint).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IEndpoint>();

            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoints(app);
            }
        }
    }
}
