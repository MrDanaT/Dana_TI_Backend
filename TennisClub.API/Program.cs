using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TennisClub.DAL;

namespace TennisClub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost? host = CreateWebHostBuilder(args).Build();

            using (IServiceScope? scope = host.Services.CreateScope())
            {
                IServiceProvider? services = scope.ServiceProvider;
                try
                {
                    TennisClubContext? context = services.GetRequiredService<TennisClubContext>();
                    // DataSeeder.Initialize(context);
                }
                catch (Exception)
                {
                    Console.WriteLine("An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .UseStartup<Startup>();
        }
    }
}