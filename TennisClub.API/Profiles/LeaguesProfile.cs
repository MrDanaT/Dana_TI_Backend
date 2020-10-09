using AutoMapper;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;

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
