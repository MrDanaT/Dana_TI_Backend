using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.LeagueRepository
{
    public interface ILeagueRepository
    {
        IEnumerable<League> GetAllLeagues();
        League GetLeagueById(int id);
    }
}