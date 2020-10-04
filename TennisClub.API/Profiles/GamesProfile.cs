using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisClub.BL.Entities;
using TennisClub.DTO.Game;

namespace TennisClub.API.Profiles
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<Game, GameReadDTO>();
            CreateMap<GameReadDTO, Game>();
            CreateMap<GameCreateDTO, Game>();
            CreateMap<Game, GameCreateDTO>();
        }
    }
}
