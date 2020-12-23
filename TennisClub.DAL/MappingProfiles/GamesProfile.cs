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
            CreateMap<Game, GameReadDTO>()
                .ForMember(self => self.MemberFullName,
                    conf => conf.MapFrom(dest => $"{dest.MemberNavigation.FirstName} {dest.MemberNavigation.LastName}"))
                .ForMember(self => self.LeagueName, conf => conf.MapFrom(dest => dest.LeagueNavigation.Name));
            CreateMap<GameUpdateDTO, Game>();
            CreateMap<GameReadDTO, Game>();
        }
    }
}