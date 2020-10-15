using AutoMapper;
using TennisClub.Common.GameResult;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class GameResultsProfile : Profile
    {
        public GameResultsProfile()
        {
            CreateMap<GameResult, GameResultReadDTO>();
            CreateMap<GameResultReadDTO, GameResult>();
            CreateMap<GameResultReadDTO, GameResultCreateDTO>();

            CreateMap<GameResultCreateDTO, GameResult>();

            CreateMap<GameResultUpdateDTO, GameResult>();
            CreateMap<GameResult, GameResultUpdateDTO>();
        }
    }
}
