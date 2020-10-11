using AutoMapper;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<Role, RoleReadDTO>();
            CreateMap<RoleCreateDTO, Role>();
            CreateMap<RoleUpdateDTO, Role>();
            CreateMap<Role, RoleUpdateDTO>();
        }
    }
}
