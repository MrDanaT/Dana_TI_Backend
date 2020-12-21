using AutoMapper;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<RoleCreateDTO, Role>();
            CreateMap<Role, RoleReadDTO>();
            CreateMap<RoleUpdateDTO, Role>();
            CreateMap<RoleReadDTO, Role>();
        }
    }
}