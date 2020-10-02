using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisClub.BL.Entities;
using TennisClub.DTO.Gender;

namespace TennisClub.API.Profiles
{
    public class GendersProfile : Profile
    {
        public GendersProfile()
        {
            // Source -> Target
            CreateMap<Gender, GenderReadDTO>();
        }
    }
}
