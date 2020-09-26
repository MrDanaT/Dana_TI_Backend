using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.RoleRepository
{
    interface IRoleRepository
    {
        void SaveChanges();

        void CreateRole(Role role);

        void UpdateRole(Role role);
    }
}
