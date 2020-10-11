using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.MemberFineServiceFolder
{
    public interface IMemberFineService
    {
        IEnumerable<MemberFine> GetAllMemberFines();
        MemberFine GetMemberFineById(int id);

        void CreateMemberFine(MemberFine memberFineCreateDto);

        void UpdateMemberFine(MemberFine memberFine);
        IEnumerable<MemberFine> GetMemberFinesByMemberId(int id);
    }
}
