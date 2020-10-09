using AutoMapper;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

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
