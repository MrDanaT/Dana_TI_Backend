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
        void GetAllMemberFines();
        void GetMemberFinesByMember(Member member);
    }
}
