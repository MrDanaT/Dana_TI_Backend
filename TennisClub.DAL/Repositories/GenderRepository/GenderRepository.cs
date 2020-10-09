using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public Gender GetGenderById(int id)
        {
            return _context.Genders.FirstOrDefault(g => g.Id == id);
        }
    }
}
