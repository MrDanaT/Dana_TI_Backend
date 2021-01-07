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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../TennisClub.API/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<TennisClubContext>();
            var connectionString = configuration.GetConnectionString("TennisClubConnection");
            builder.UseSqlServer(connectionString);
            return new TennisClubContext(builder.Options);
        }
    }
}