using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TennisClub.BL.RoleServiceFolder;
using TennisClub.Common;
using TennisClub.Common.Role;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IRoleService _service;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService service, ILogger<RolesController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/roles
        [HttpGet]
        public ActionResult<IEnumerable<RoleReadDTO>> GetAllRoles()
        {
            try
            {
                var roleItems = _service.GetAllRoles();
                return Ok(roleItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/roles/5
        [HttpGet("{id}", Name = "GetRoleById")]
        public ActionResult<RoleReadDTO> GetRoleById(int id)
        {
            try
            {
                var roleItem = _service.GetRoleById(id);

                if (roleItem.IsNull()) return NotFound();

                return Ok(roleItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/roles
        [HttpPost]
        public ActionResult<RoleReadDTO> CreateRole(RoleCreateDTO roleCreateDTO)
        {
            try
            {
                var createdRole = _service.CreateRole(roleCreateDTO);
                return CreatedAtRoute(nameof(GetRoleById), new {createdRole.Id}, createdRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


        // PUT: api/roles/5 
        [HttpPut("{id}")]
        public ActionResult UpdateRole(int id, RoleUpdateDTO updateDTO)
        {
            try
            {
                var roleModelFromRepo = _service.GetRoleById(id);

                if (roleModelFromRepo.IsNull()) return NotFound();

                _service.UpdateRole(id, updateDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}