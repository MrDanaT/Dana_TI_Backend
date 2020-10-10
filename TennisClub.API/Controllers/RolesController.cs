using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.RoleRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly RoleLogic _logic;
        private readonly IMapper _mapper;

        public RolesController(RoleLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleReadDTO>> GetAllRoles()
        {
            IEnumerable<Role> roleItems = _logic.GetAllRoles();

            return Ok(_mapper.Map<IEnumerable<RoleReadDTO>>(roleItems));
        }

        // GET: api/roles/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RoleReadDTO> GetRoleById(int id)
        {
            Role roleItem = _logic.GetRoleById(id);

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

            _logic.CreateRole(roleModel);

            RoleReadDTO roleReadDTO = _mapper.Map<RoleReadDTO>(roleModel);

            return CreatedAtRoute(nameof(GetRoleById), new { roleReadDTO.Id }, roleReadDTO);
        }


        // PATCH: api/roles/5 
        [HttpPatch("{id}")]
        public ActionResult<Role> PartialRoleUpdate(int id, JsonPatchDocument<RoleUpdateDTO> patchDoc)
        {
            Role roleModelFromRepo = _logic.GetRoleById(id);

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

            _logic.PartialRoleUpdate(roleModelFromRepo);

            return NoContent();
        }
    }
}
