using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepositoryFolder
{
    public interface IMemberFineRepository : IRepository<MemberFine>
    {
        IEnumerable<MemberFine> GetMemberFinesByMember(Member member);
    }
}
