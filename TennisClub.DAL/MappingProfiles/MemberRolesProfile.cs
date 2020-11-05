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
            CreateMap<MemberRole, MemberRoleReadDTO>();
            CreateMap<MemberRoleUpdateDTO, MemberRole>();
            CreateMap<MemberRoleReadDTO, MemberRole>();
        }
    }
}
