using AutoMapper;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepositoryFolder
{
    public class RoleRepository : Repository<Role, RoleCreateDTO, RoleReadDTO, RoleUpdateDTO, byte>, IRoleRepository
    {
        public RoleRepository(TennisClubContext context, IMapper mapper)
          : base(context, mapper)
        { }


    }
}
