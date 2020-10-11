using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.MemberRoleServiceFolder
{
    public interface IMemberRoleService
    {
        IEnumerable<MemberRole> GetAllMemberRoles();
        MemberRole GetMemberRoleById(int id);
        void CreateMemberRole(MemberRole memberRoleCreateDTO);
        void UpdateMemberRole(MemberRole memberRole);
        IEnumerable<Role> GetRolesByMemberId(int id);
        IEnumerable<Member> GetMembersByRoles(List<Role> roleCreateDTOs);
    }
}
