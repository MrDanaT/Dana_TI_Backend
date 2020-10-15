using AutoMapper;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<Member, MemberReadDTO>();
            CreateMap<MemberReadDTO, Member>();
            CreateMap<MemberReadDTO, MemberCreateDTO>();

            CreateMap<MemberCreateDTO, Member>();

            CreateMap<MemberUpdateDTO, Member>();
            CreateMap<Member, MemberUpdateDTO>();
        }
    }
}
