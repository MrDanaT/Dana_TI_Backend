using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepository
{
    public interface IMemberRoleRepository : ISavable
    {
        void CreateMemberRole(MemberRole memberRole);
        void UpdateMemberRole(MemberRole memberRole);
        IEnumerable<Member> GetMembersByRoles(params Role[] roles);
        IEnumerable<Role> GetRolesByMember(Member member);
    }
}
