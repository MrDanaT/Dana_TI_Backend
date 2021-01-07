using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace TennisClub.API
{
    public static class APIExtension
    {
        public static IServiceCollection AddAPIControllers(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            return services;
        }

        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder => { loggingBuilder.AddFile("app.log", true); });

            return services;
        }
    }
}