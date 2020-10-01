using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TennisClubContext _context;

        public RoleRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void CreateRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            _context.Roles.Add(role);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.AsNoTracking().ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateRole(Role role)
        {
            //Nothing
        }
    }
}
