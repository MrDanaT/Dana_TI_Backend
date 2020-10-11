using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepositoryFolder
{
    public interface IMemberRepository : IRepository<Member>
    {
        IEnumerable<Member> GetAllActiveMembers();
    }
}
