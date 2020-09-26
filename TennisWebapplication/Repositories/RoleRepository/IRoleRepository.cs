using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.RoleRepository
{
    public interface IRoleRepository : ISavable
    {
        void CreateRole(Role role);

        void UpdateRole(Role role);
    }
}
