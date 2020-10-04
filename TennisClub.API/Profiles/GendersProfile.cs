using AutoMapper;
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
