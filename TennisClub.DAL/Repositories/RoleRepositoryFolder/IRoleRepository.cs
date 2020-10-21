using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepositoryFolder
{
    public interface IRoleRepository : IRepository<RoleCreateDTO, RoleReadDTO, RoleUpdateDTO, byte>
    {
    }
}
