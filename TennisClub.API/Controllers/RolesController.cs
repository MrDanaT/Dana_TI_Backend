using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TennisClub.BL.RoleServiceFolder;
using TennisClub.Common.Role;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        // GET: api/roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleReadDTO>> GetAllRoles()
        {
            var roleItems = _service.GetAllRoles();

            return Ok(roleItems);
        }

        // GET: api/roles/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RoleReadDTO> GetRoleById(int id)
        {
            var roleItem = _service.GetRoleById(id);

            if (roleItem == null) return NotFound();

            return Ok(roleItem);
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult<RoleReadDTO> CreateRole(RoleCreateDTO roleCreateDTO)
        {
            var createdRole = _service.CreateRole(roleCreateDTO);

            return CreatedAtRoute(nameof(GetRoleById), new {createdRole.Id}, createdRole);
        }


        // PUT: api/roles/5 
        [HttpPut("{id}")]
        public ActionResult UpdateRole(int id, RoleUpdateDTO updateDTO)
        {
            var roleModelFromRepo = _service.GetRoleById(id);

            if (roleModelFromRepo == null) return NotFound();

            _service.UpdateRole(id, updateDTO);

            return NoContent();
        }
    }
}