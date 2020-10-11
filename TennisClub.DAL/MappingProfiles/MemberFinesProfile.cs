using AutoMapper;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
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
