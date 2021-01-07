using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TennisClub.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TennisClubContext>
    {
        public TennisClubContext CreateDbContext(string[] args)
        {
            IConfigurationRoot? configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../TennisClub.API/appsettings.json")
                .Build();
            DbContextOptionsBuilder<TennisClubContext>? builder = new DbContextOptionsBuilder<TennisClubContext>();
            string? connectionString = configuration.GetConnectionString("TennisClubConnection");
            builder.UseSqlServer(connectionString);
            return new TennisClubContext(builder.Options);
        }
    }
}