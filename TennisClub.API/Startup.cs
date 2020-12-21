using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using TennisClub.API;
using TennisClub.BL;
using TennisClub.BL.GameResultServiceFolder;
using TennisClub.BL.GameServiceFolder;
using TennisClub.BL.GenderServiceFolder;
using TennisClub.BL.LeagueServiceFolder;
using TennisClub.BL.MemberFineServiceFolder;
using TennisClub.BL.MemberRoleServiceFolder;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.BL.RoleServiceFolder;
using TennisClub.DAL;
using TennisClub.DAL.Repositories;

namespace TennisClub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // API
            services.AddAPIControllers();

            // BL
            services.AddServices();

            // DAL
            services.AddUnitOfWork();
            services.RegisterAutoMapper();
            services.RegisterContext(Configuration.GetConnectionString("TennisClubConnection"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
