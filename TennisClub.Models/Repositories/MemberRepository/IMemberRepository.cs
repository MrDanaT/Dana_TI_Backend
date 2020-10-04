using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepository
{
    public interface IMemberRepository : IUpdatable
    {
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
        IEnumerable<Member> GetAllMembers();
        Member GetMemberById(int id);
    }
}
