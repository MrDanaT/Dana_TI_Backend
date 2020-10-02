using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepository
{
    public class GenderRepository : IGenderRepository
    {
        private readonly TennisClubContext _context;

        public GenderRepository(TennisClubContext context)
        {
            _context = context;
        }

        public IEnumerable<Gender> GetAllGenders()
        {
            return _context.Genders.AsNoTracking().ToList();
        }
    }
}
