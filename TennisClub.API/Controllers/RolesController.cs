using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            IEnumerable<RoleReadDTO> roleItems = _service.GetAllRoles();

            return Ok(roleItems);
        }

        // GET: api/roles/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RoleReadDTO> GetRoleById(int id)
        {
            RoleReadDTO roleItem = _service.GetRoleById(id);

            if (roleItem == null)
            {
                return NotFound();
            }

            return Ok(roleItem);
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult<RoleReadDTO> CreateRole(RoleCreateDTO roleCreateDTO)
        {
            RoleReadDTO createdRole = _service.CreateRole(roleCreateDTO);

            return CreatedAtRoute(nameof(GetRoleById), new { createdRole.Id }, createdRole);
        }


        // PATCH: api/roles/5 
        [HttpPatch("{id}")]
        public ActionResult UpdateRole(byte id, JsonPatchDocument<RoleUpdateDTO> patchDoc)
        {
            RoleReadDTO roleModelFromRepo = _service.GetRoleById(id);

            if (roleModelFromRepo == null)
            {
                return NotFound();
            }

            RoleUpdateDTO roleToPatch = _service.GetUpdateDTOByReadDTO(roleModelFromRepo);
            patchDoc.ApplyTo(roleToPatch, ModelState);

            if (!TryValidateModel(roleToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _service.UpdateRole(roleToPatch, roleModelFromRepo);

            return NoContent();
        }
    }
}
