using System.Collections.Generic;
using TennisClub.Common.Role;

namespace TennisClub.BL.RoleServiceFolder
{
    public interface IRoleService
    {
        IEnumerable<RoleReadDTO> GetAllRoles();

        RoleReadDTO GetRoleById(int id);

        RoleReadDTO CreateRole(RoleCreateDTO role);

        void UpdateRole(int id, RoleUpdateDTO updateDTO);
    }
}