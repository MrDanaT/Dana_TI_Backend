using AutoMapper;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.LeagueRepositoryFolder
{
    public class LeagueRepository : Repository<League, object, LeagueReadDTO, object, byte>, ILeagueRepository
    {
        public LeagueRepository(TennisClubContext context, IMapper mapper)
           : base(context, mapper)
        { }


    }
}
