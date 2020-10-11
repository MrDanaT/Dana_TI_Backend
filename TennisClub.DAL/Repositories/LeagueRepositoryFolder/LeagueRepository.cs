using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.LeagueRepositoryFolder
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        public LeagueRepository(TennisClubContext context)
           : base(context)
        { }


    }
}
