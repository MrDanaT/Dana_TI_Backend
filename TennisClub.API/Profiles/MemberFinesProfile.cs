using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisClub.BL.Entities;
using TennisClub.DTO.MemberFine;

namespace TennisClub.API.Profiles
{
    public class MemberFinesProfile : Profile
    {
        public MemberFinesProfile()
        {
            CreateMap<MemberFine, MemberFineReadDTO>();
            CreateMap<MemberFineReadDTO, MemberFine>();
            CreateMap<MemberFineCreateDTO, MemberFine>();
            CreateMap<MemberFine, MemberFineCreateDTO>();
        }
    }
}
