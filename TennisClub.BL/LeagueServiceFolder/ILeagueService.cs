using System.Collections.Generic;
using TennisClub.Common.League;

namespace TennisClub.BL.LeagueServiceFolder
{
    public interface ILeagueService
    {
        IEnumerable<LeagueReadDTO> GetAllLeagues();

        LeagueReadDTO GetLeagueById(int id);
    }
}