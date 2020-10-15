using System.Collections.Generic;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.LeagueServiceFolder
{
    public interface ILeagueService
    {
        IEnumerable<LeagueReadDTO> GetAllLeagues();

        LeagueReadDTO GetLeagueById(byte id);
    }
}
