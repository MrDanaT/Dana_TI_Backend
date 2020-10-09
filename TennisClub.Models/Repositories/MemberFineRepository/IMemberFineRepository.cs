using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepository
{
    public interface IMemberFineRepository : IUpdatable<MemberFine>
    {
        IEnumerable<MemberFine> GetAllMemberFines();
        IEnumerable<MemberFine> GetMemberFinesByMember(Member member);
        MemberFine GetMemberFineById(int id);
    }
}
