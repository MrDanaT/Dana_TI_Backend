using AutoMapper;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class LeaguesProfile : Profile
    {
        public LeaguesProfile()
        {
            CreateMap<League, LeagueReadDTO>();
        }
    }
}
