using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace TennisClub.API
{
    public static class APIExtension
    {
        public static void AddAPIControllers(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }
    }
}