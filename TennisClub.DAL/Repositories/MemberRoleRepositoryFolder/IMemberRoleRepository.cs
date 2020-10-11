using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public interface IMemberRoleRepository : IRepository<MemberRole>
    {
        IEnumerable<Member> GetMembersByRoles(List<Role> roles);
        IEnumerable<Role> GetRolesByMember(Member member);
    }
}
