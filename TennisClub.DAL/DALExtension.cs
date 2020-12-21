using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TennisClub.DAL.Repositories;

namespace TennisClub.DAL
{
    public static class DALExtension
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TennisClubContext>(opt => opt.UseSqlServer(connectionString));
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}