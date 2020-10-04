using AutoMapper;
using TennisClub.BL.Entities;
using TennisClub.DTO.GameResult;

namespace TennisClub.API.Profiles
{
    public class GameResultsProfile : Profile
    {
        public GameResultsProfile()
        {
            CreateMap<GameResult, GameResultReadDTO>();
            CreateMap<GameResultReadDTO, GameResult>();
            CreateMap<GameResultCreateDTO, GameResult>();
            CreateMap<GameResult, GameResultCreateDTO>();
        }
    }
}
