using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.LeagueServiceFolder
{
    public interface ILeagueService
    {
        IEnumerable<League> GetAllLeagues();

        League GetLeagueById(byte id);
    }
}
