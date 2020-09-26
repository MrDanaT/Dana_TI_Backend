using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.MemberRoleRepository
{
    internal interface IMemberRoleRepository : ISavable
    {
        void AssignMemberRole(MemberRole role);
        void RetractMemberRole(MemberRole role);
        void GetMembersRolesByRole(params Role[] role);
        void GetMemberRolesByMember(Member member);
    }
}
