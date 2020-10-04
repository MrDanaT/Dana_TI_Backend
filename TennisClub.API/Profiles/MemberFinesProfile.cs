using AutoMapper;
using TennisClub.BL.Entities;
using TennisClub.DTO.MemberFine;

namespace TennisClub.API.Profiles
{
    public class MemberFinesProfile : Profile
    {
        public MemberFinesProfile()
        {
            CreateMap<MemberFine, MemberFineReadDTO>();
            CreateMap<MemberFineReadDTO, MemberFine>();
            CreateMap<MemberFineCreateDTO, MemberFine>();
            CreateMap<MemberFine, MemberFineCreateDTO>();
        }
    }
}
