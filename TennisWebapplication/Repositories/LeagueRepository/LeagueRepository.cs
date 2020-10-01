using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.LeagueRepository
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
