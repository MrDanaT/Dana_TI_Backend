using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.LeagueRepository
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly TennisClubContext _context;

        public LeagueRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(League entity)
        {
            // Nothing
        }

        public void Delete(League entity)
        {
            // Nothing
        }

        public IEnumerable<League> GetAll()
        {
            return _context.Leagues.AsNoTracking().ToList();
        }

        public League GetById(int id)
        {
            return _context.Leagues.FirstOrDefault(l => l.Id == id);
        }

        public bool SaveChanges()
        {
            return false;
        }

        public void Update(League entity)
        {
            // Nothing
        }
    }
}
