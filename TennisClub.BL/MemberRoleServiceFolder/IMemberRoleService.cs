using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.MemberRoleServiceFolder
{
    public interface IMemberRoleService
    {
        IEnumerable<MemberRoleReadDTO> GetAllMemberRoles();
        MemberRoleReadDTO GetMemberRoleById(int id);
        MemberRoleReadDTO CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO);
        void UpdateMemberRole(MemberRoleUpdateDTO memberRoleToPatch, MemberRoleReadDTO  memberRoleModelFromRepo);
        IEnumerable<RoleReadDTO> GetRolesByMemberId(int id);
        IEnumerable<MemberReadDTO> GetMembersByRoles(List<RoleReadDTO> roleCreateDTOs);
        MemberRoleUpdateDTO GetUpdateDTOByReadDTO(MemberRoleReadDTO entity);
    }
}
