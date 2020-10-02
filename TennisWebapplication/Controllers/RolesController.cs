using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.RoleRepository;

namespace TennisWebapplication.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleRepository _repo;

        public RolesController(IRoleRepository repo)
        {
            _repo = repo;
        }

        public ActionResult<IEnumerable<Role>> GetAllRoles()
        {
            IEnumerable<Role> roleItems = _repo.GetAllRoles();

            return Ok(roleItems);
        }

        // Test, vervangen door DTO's.
        public ActionResult<Role> CreateRole(Role role)
        {
            _repo.CreateRole(role);
            _repo.SaveChanges();

            return Ok(role);
        }

        // Test, vervangen door DTO's.
        public ActionResult<Role> UpdateRole(int id, Role role)
        {
            Role roleFromRepo = _repo.GetRoleById(id);

            if (roleFromRepo == null)
            {
                return NotFound();
            }

            // TODO: do the changes here

            _repo.UpdateRole(role);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}
