using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepositoryFolder
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(TennisClubContext context)
          : base(context)
        { }

    }
}
