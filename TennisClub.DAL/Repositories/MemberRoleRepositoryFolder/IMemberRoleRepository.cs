using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public interface IMemberRoleRepository : IRepository<MemberRoleCreateDTO, MemberRoleReadDTO, MemberRoleUpdateDTO>
    {
        IEnumerable<MemberRoleReadDTO> GetMemberRolesByRoles(List<RoleReadDTO> roles);
        IEnumerable<MemberRoleReadDTO> GetMemberRolesByMember(MemberReadDTO member);
    }
}
