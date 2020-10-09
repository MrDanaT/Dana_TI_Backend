using AutoMapper;

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
