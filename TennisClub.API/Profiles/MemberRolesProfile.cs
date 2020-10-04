using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisClub.BL.Entities;
using TennisClub.DTO.MemberRole;

namespace TennisClub.API.Profiles
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
