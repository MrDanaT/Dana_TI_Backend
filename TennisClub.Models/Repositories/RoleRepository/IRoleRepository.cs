using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepository
{
    public interface IRoleRepository : ISavable
    {
        void CreateRole(Role role);

        void UpdateRole(Role role);
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int id);
    }
}
