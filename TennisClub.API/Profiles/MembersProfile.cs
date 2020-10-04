using AutoMapper;
using TennisClub.BL.Entities;
using TennisClub.DTO.Member;

namespace TennisClub.API.Profiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<Member, MemberReadDTO>();
            CreateMap<MemberCreateDTO, Member>();
            CreateMap<MemberUpdateDTO, Member>();
            CreateMap<Member, MemberUpdateDTO>();
        }
    }
}
