using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.MemberRoleRepository
{
    public interface IMemberRoleRepository : ISavable
    {
        void CreateMemberRole(MemberRole role);
        void UpdateMemberRole(MemberRole role);
        IEnumerable<Member> GetMembersByRole(params Role[] role);
        IEnumerable<MemberRole> GetMemberRolesByMember(Member member);
    }
}
