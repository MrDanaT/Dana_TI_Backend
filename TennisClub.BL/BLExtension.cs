using Microsoft.Extensions.DependencyInjection;
using TennisClub.BL.GameResultServiceFolder;
using TennisClub.BL.GameServiceFolder;
using TennisClub.BL.GenderServiceFolder;
using TennisClub.BL.LeagueServiceFolder;
using TennisClub.BL.MemberFineServiceFolder;
using TennisClub.BL.MemberRoleServiceFolder;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.BL.RoleServiceFolder;

namespace TennisClub.BL
{
    public static class BLExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IGameResultService, GameResultService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ILeagueService, LeagueService>();
            services.AddTransient<IMemberFineService, MemberFineService>();
            services.AddTransient<IMemberRoleService, MemberRoleService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IRoleService, RoleService>();
            return services;
        }
    }
}
