using AutoMapper;
using TennisClub.BL.Entities;
using TennisClub.DTO.Role;

namespace TennisClub.API.Profiles
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
