using AutoMapper;

namespace TennisClub.API.Profiles
{
    public class LeaguesProfile : Profile
    {
        public LeaguesProfile()
        {
            CreateMap<League, LeagueReadDTO>();
        }
    }
}
