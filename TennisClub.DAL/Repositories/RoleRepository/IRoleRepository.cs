using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepository
{
    public interface IRoleRepository : IUpdatable<Role>
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int id);
    }
}
