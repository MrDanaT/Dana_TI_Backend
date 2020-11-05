using AutoMapper;
using TennisClub.Common.Game;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<GameCreateDTO, Game>();
            CreateMap<Game, GameReadDTO>();
            CreateMap<GameUpdateDTO, Game>();
            CreateMap<GameReadDTO, Game>();
        }
    }
}
