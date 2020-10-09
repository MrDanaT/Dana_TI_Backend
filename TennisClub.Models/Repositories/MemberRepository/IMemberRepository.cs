using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepository
{
    public interface IMemberRepository : IUpdatable<Member>, IDeletable<Member>
    {
        IEnumerable<Member> GetAllMembers();
        Member GetMemberById(int id);
        IEnumerable<Member> GetAllActiveMembers();
    }
}
