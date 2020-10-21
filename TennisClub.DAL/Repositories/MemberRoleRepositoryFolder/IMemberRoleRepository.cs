using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public interface IMemberRoleRepository : IRepository< MemberRoleCreateDTO, MemberRoleReadDTO, MemberRoleUpdateDTO, int>
    {
        IEnumerable<MemberReadDTO> GetMembersByRoles(List<RoleReadDTO> roles);
        IEnumerable<RoleReadDTO> GetRolesByMember(MemberReadDTO member);
    }
}
