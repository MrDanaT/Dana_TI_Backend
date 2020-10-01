using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.MemberRepository
{
    public interface IMemberRepository : ISavable
    {
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
        IEnumerable<Member> GetAllMembers();
    }
}
