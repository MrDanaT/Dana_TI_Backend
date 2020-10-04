using AutoMapper;
using TennisClub.BL.Entities;
using TennisClub.DTO.League;

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
