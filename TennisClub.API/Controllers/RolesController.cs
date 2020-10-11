using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.RoleServiceFolder;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IRoleService _service;
        private readonly IMapper _mapper;

        public RolesController(IRoleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleReadDTO>> GetAllRoles()
        {
            IEnumerable<Role> roleItems = _service.GetAllRoles();

            return Ok(_mapper.Map<IEnumerable<RoleReadDTO>>(roleItems));
        }

        // GET: api/roles/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RoleReadDTO> GetRoleById(byte id)
        {
            Role roleItem = _service.GetRoleById(id);

            if (roleItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RoleReadDTO>(roleItem));
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult<RoleReadDTO> CreateRole(RoleCreateDTO roleCreateDTO)
        {
            Role roleModel = _mapper.Map<Role>(roleCreateDTO);

            _service.CreateRole(roleModel);

            RoleReadDTO roleReadDTO = _mapper.Map<RoleReadDTO>(roleModel);

            return CreatedAtRoute(nameof(GetRoleById), new { roleReadDTO.Id }, roleReadDTO);
        }


        // PATCH: api/roles/5 
        [HttpPatch("{id}")]
        public ActionResult<Role> UpdateRole(byte id, JsonPatchDocument<RoleUpdateDTO> patchDoc)
        {
            Role roleModelFromRepo = _service.GetRoleById(id);

            if (roleModelFromRepo == null)
            {
                return NotFound();
            }

            RoleUpdateDTO roleToPatch = _mapper.Map<RoleUpdateDTO>(roleModelFromRepo);
            patchDoc.ApplyTo(roleToPatch, ModelState);

            if (!TryValidateModel(roleToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(roleToPatch, roleModelFromRepo);

            _service.UpdateRole(roleModelFromRepo);

            return NoContent();
        }
    }
}
