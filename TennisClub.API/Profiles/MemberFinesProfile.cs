using AutoMapper;

namespace TennisClub.API.Profiles
{
    public class MemberFinesProfile : Profile
    {
        public MemberFinesProfile()
        {
            CreateMap<MemberFine, MemberFineReadDTO>();
            CreateMap<MemberFineCreateDTO, MemberFine>();
            CreateMap<MemberFineUpdateDTO, MemberFine>();
            CreateMap<MemberFine, MemberFineUpdateDTO>();
        }
    }
}
