using AutoMapper;

namespace TennisClub.API.Profiles
{
    public class GameResultsProfile : Profile
    {
        public GameResultsProfile()
        {
            CreateMap<GameResult, GameResultReadDTO>();
            CreateMap<GameResultCreateDTO, GameResult>();
            CreateMap<GameResultUpdateDTO, GameResult>();
            CreateMap<GameResult, GameResultUpdateDTO>();
        }
    }
}
