using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
