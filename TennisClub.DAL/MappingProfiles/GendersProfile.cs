using AutoMapper;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
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
