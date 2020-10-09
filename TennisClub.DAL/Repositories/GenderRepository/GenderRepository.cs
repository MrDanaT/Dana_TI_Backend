using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepository
{
    public class GenderRepository : IGenderRepository
    {
        private readonly TennisClubContext _context;

        public GenderRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(Gender entity)
        {
            // Nothing
        }

        public void Delete(Gender entity)
        {
            // Nothing
        }

        public IEnumerable<Gender> GetAll()
        {
            return _context.Genders.AsNoTracking().ToList();
        }

        public Gender GetById(int id)
        {
            return _context.Genders.FirstOrDefault(g => g.Id == id);
        }

        public bool SaveChanges()
        {
            return false;
        }

        public void Update(Gender entity)
        {
            // Nothing
        }
    }
}
