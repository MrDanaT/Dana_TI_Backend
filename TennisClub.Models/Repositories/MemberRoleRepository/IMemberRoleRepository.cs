using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepository
{
    public interface IMemberRoleRepository : IUpdatable
    {
        void CreateMemberRole(MemberRole memberRole);
        void UpdateMemberRole(MemberRole memberRole);
        IEnumerable<Member> GetMembersByRoles(IEnumerable<Role> roles);
        IEnumerable<Role> GetRolesByMember(Member member);
    }
}
