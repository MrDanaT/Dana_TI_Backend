using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.MemberServiceFolder
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAllMembers();
        Member GetMemberById(int id);
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
        IEnumerable<Member> GetAllActiveMembers();
    }
}
