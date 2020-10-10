using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepository
{
    public interface IMemberRoleRepository : IRepository<MemberRole>
    {
        IEnumerable<Member> GetMembersByRoles(List<Role> roles);
        IEnumerable<Role> GetRolesByMember(Member member);
    }
}
