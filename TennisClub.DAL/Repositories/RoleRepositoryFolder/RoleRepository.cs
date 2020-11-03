using AutoMapper;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepositoryFolder
{
    public class RoleRepository : Repository<Role, RoleCreateDTO, RoleReadDTO, RoleUpdateDTO>, IRoleRepository
    {
        public RoleRepository(TennisClubContext context, IMapper mapper)
          : base(context, mapper)
        { }

        public override void Delete(int id)
        {
            // Do nothing
        }
    }
}
