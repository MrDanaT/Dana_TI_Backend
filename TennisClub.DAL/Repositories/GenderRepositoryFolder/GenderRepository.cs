using AutoMapper;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepositoryFolder
{
    public class GenderRepository : Repository<Gender, object, GenderReadDTO, object>, IGenderRepository
    {
        public GenderRepository(TennisClubContext context, IMapper mapper)
          : base(context, mapper)
        { }

    }
}
