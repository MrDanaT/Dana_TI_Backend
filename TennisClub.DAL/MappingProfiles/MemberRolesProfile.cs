using AutoMapper;
using TennisClub.Common.MemberRole;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class MemberRolesProfile : Profile
    {
        public MemberRolesProfile()
        {
            CreateMap<MemberRole, MemberRoleReadDTO>();
            CreateMap<MemberRoleCreateDTO, MemberRole>();
            CreateMap<MemberRoleUpdateDTO, MemberRole>();
            CreateMap<MemberRole, MemberRoleUpdateDTO>();
        }
    }
}
