using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepository
{
    public interface IMemberFineRepository : ISavable
    {
        void CreateMemberFine(MemberFine memberFine);
        void UpdateMemberFine(MemberFine memberFine);
        IEnumerable<MemberFine> GetAllMemberFines();
        IEnumerable<MemberFine> GetMemberFinesByMember(Member member);
    }
}
