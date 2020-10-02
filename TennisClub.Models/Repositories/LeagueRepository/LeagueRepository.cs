using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.LeagueRepository
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly TennisClubContext _context;

        public LeagueRepository(TennisClubContext context)
        {
            _context = context;
        }

        public IEnumerable<League> GetAllLeagues()
        {
            return _context.Leagues.AsNoTracking().ToList();
        }
    }
}
