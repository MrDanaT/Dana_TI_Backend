using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TennisClubContext _context;

        public RoleRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            _context.Roles.Add(role);
        }

        public void Delete(Role entity)
        {
            // Nothing
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.AsNoTracking().ToList();
        }

        public Role GetById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(Role role)
        {
            //Nothing
        }
    }
}
