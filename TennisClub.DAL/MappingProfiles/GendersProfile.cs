using AutoMapper;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class GendersProfile : Profile
    {
        public GendersProfile()
        {
            CreateMap<Gender, GenderReadDTO>();
            CreateMap<GenderReadDTO, Gender>();
        }
    }
}
