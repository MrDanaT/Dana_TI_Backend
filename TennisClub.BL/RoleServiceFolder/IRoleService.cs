using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.RoleServiceFolder
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();

        Role GetRoleById(byte id);

        void CreateRole(Role role);

        void UpdateRole(Role role);
    }
}
