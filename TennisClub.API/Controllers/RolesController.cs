using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.RoleRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IRoleRepository _repo;

        public RolesController(IRoleRepository repo)
        {
            _repo = repo;
        }

        // GET: api/roles
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAllRoles()
        {
            IEnumerable<Role> roleItems = _repo.GetAllRoles();

            return Ok(roleItems);
        }

        // GET: api/roles/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<Role> GetRoleById(int id)
        {
            Role commandItem = _repo.GetRoleById(id);

            if (commandItem != null)
            {
                return Ok(commandItem);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult<Role> CreateRole(Role role)
        {
            _repo.CreateRole(role);
            _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetRoleById), new { role.Id }, role);
        }


        // PUT: api/roles/5 
        [HttpPut("{id}")]
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
