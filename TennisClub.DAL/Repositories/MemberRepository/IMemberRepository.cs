using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepository
{
    public interface IMemberRepository : IRepository<Member>
    {
        IEnumerable<Member> GetAllActiveMembers();
    }
}
