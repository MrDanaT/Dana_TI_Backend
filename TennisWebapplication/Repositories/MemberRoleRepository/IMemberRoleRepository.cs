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
        void CreateMemberRole(MemberRole memberRole);
        void UpdateMemberRole(MemberRole memberRole);
        IEnumerable<Member> GetMembersByRole(params Role[] roles);
        IEnumerable<Role> GetRolesByMember(Member member);
    }
}
