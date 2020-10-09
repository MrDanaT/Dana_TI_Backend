using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepository
{
    public interface IMemberFineRepository : IRepository<MemberFine>
    {
        IEnumerable<MemberFine> GetMemberFinesByMember(Member member);
    }
}
