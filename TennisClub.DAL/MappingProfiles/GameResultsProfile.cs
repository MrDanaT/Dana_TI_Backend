using AutoMapper;
using TennisClub.Common.GameResult;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class GameResultsProfile : Profile
    {
        public GameResultsProfile()
        {
            CreateMap<GameResultCreateDTO, GameResult>();
            CreateMap<GameResult, GameResultReadDTO>();
            CreateMap<GameResultReadDTO, GameResult>();
            CreateMap<GameResultUpdateDTO, GameResult>();
        }
    }
}