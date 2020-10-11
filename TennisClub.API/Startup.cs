using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
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
            services.AddDbContext<TennisClubContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("TennisClubConnection")));

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IGameResultService, GameResultService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ILeagueService, LeagueService>();
            services.AddTransient<IMemberFineService, MemberFineService>();
            services.AddTransient<IMemberRoleService, MemberRoleService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IRoleService, RoleService>();
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
