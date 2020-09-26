using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.GenderRepository
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
            return _context.Genders.ToList();
        }

        public Gender GetGenderById(byte id)
        {
            return _context.Genders.FirstOrDefault(g => g.Id == id);
        }
    }
}
