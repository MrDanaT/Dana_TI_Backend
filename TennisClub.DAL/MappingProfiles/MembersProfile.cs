using AutoMapper;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.MappingProfiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<MemberCreateDTO, Member>();
            CreateMap<Member, MemberReadDTO>();
            CreateMap<MemberUpdateDTO, Member>();
            CreateMap<MemberReadDTO, Member>();
        }
    }
}
