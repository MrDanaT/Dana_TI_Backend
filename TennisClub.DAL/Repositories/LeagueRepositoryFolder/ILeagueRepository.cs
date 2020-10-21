using TennisClub.Common.League;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.LeagueRepositoryFolder
{
    public interface ILeagueRepository : IRepository<object, LeagueReadDTO, object, byte>
    {
    }
}