using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.RoleRepository;

namespace TennisClub.BL
{
    public class RoleLogic
    {
        private readonly IRoleRepository _repo;

        public RoleLogic(IRoleRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<Role> roleItems = _repo.GetAll();

            return roleItems;
        }

        public Role GetRoleById(int id)
        {
            Role roleItem = _repo.GetById(id);

            return roleItem;
        }

        public void CreateRole(Role role)
        {
            _repo.Create(role);
            _repo.SaveChanges();
        }

        public void PartialRoleUpdate(Role role)
        {
            _repo.Update(role);
            _repo.SaveChanges();
        }
    }
}
