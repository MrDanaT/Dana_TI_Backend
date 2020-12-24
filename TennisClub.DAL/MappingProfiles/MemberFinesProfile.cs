using AutoMapper;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class MemberFinesProfile : Profile
    {
        public MemberFinesProfile()
        {
            CreateMap<MemberFineCreateDTO, MemberFine>();
            CreateMap<MemberFine, MemberFineReadDTO>()
                .ForMember(self => self.MemberFullName, conf => conf.MapFrom(dest => $"{dest.MemberNavigation.FirstName} {dest.MemberNavigation.LastName}")); ;
            CreateMap<MemberFineUpdateDTO, MemberFine>();
            CreateMap<MemberFineReadDTO, MemberFine>();
        }
    }
}