using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepositoryFolder
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(TennisClubContext context)
          : base(context)
        { }


    }
}
