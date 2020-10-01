using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.MemberFineRepository
{
    public interface IMemberFineRepository : ISavable
    {
        void CreateMemberFine(MemberFine memberFine);
        void UpdateMemberFine(MemberFine memberFine);
        IEnumerable<MemberFine> GetAllMemberFines();
        IEnumerable<MemberFine> GetMemberFinesByMember(Member member);
    }
}
