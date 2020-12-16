using AutoMapper;
using TennisClub.Common.MemberRole;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class MemberRolesProfile : Profile
    {
        public MemberRolesProfile()
        {
            CreateMap<MemberRoleCreateDTO, MemberRole>();
            CreateMap<MemberRole, MemberRoleReadDTO>()
                .ForMember(self => self.RoleName, conf => conf.MapFrom(dest => dest.RoleNavigation.Name))
                .ForMember(self => self.MemberFullName, conf => conf.MapFrom(dest => $"{dest.MemberNavigation.FirstName} {dest.MemberNavigation.LastName}"));
            CreateMap<MemberRoleUpdateDTO, MemberRole>();
            CreateMap<MemberRoleReadDTO, MemberRole>();
        }
    }
}
