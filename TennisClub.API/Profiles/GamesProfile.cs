using AutoMapper;
using TennisClub.Common.Game;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Profiles
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<Game, GameReadDTO>();
            CreateMap<GameCreateDTO, Game>();
            CreateMap<GameUpdateDTO, Game>();
            CreateMap<Game, GameUpdateDTO>();
        }
    }
}
