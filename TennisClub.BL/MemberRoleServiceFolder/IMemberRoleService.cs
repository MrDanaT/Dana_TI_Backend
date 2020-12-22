using System.Collections.Generic;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;

namespace TennisClub.BL.MemberRoleServiceFolder
{
    public interface IMemberRoleService
    {
        IEnumerable<MemberRoleReadDTO> GetAllMemberRoles();
        MemberRoleReadDTO GetMemberRoleById(int id);
        MemberRoleReadDTO CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO);
        void UpdateMemberRole(int id, MemberRoleUpdateDTO updateDTO);
        IEnumerable<MemberRoleReadDTO> GetMemberRolesByMemberId(int id);
        IEnumerable<MemberRoleReadDTO> GetMemberRolesByRoleIds(string roleIds);
    }
}