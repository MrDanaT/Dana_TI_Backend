using System.Collections.Generic;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.RoleServiceFolder
{
    public interface IRoleService
    {
        IEnumerable<RoleReadDTO> GetAllRoles();

        RoleReadDTO GetRoleById(byte id);

        RoleReadDTO CreateRole(RoleCreateDTO role);

        void UpdateRole(RoleUpdateDTO roleToPatch, RoleReadDTO roleModelFromRepo);
        RoleUpdateDTO GetUpdateDTOByReadDTO(RoleReadDTO entity);
    }
}
