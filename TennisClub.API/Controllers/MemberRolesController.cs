using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using TennisClub.BL.MemberRoleServiceFolder;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.Common;
using TennisClub.Common.MemberRole;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRolesController : Controller
    {
        private readonly IMemberRoleService _service;
        private readonly ILogger<MemberRolesController> _logger;

        public MemberRolesController(IMemberRoleService service, ILogger<MemberRolesController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/memberroles
        [HttpGet]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetAllMemberRoles()
        {
            try
            {
                var memberRoleItems = _service.GetAllMemberRoles();
                return Ok(memberRoleItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET api/memberroles/5
        [HttpGet("{id}", Name = "GetMemberRoleById")]
        public ActionResult<MemberRoleReadDTO> GetMemberRoleById(int id)
        {
            try
            {
                var memberRoleItem = _service.GetMemberRoleById(id);

                if (memberRoleItem.IsNull()) return NotFound();

                return Ok(memberRoleItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/memberroles
        [HttpPost]
        public ActionResult<MemberRoleReadDTO> CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO)
        {
            try
            {
                var createdMemberRole = _service.CreateMemberRole(memberRoleCreateDTO);
                return CreatedAtRoute(nameof(GetMemberRoleById), new {createdMemberRole.Id}, createdMemberRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // PATCH api/memberroles/5
        [HttpPut("{id}")]
        public ActionResult UpdateMemberRole(int id, MemberRoleUpdateDTO updateDTO)
        {
            try
            {
                var memberRoleModelFromRepo = _service.GetMemberRoleById(id);

                if (memberRoleModelFromRepo.IsNull()) return NotFound();

                _service.UpdateMemberRole(id, updateDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/memberroles/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetRolesByMemberId(int id)
        {
            try
            {
                IEnumerable<MemberRoleReadDTO> roleItems = _service.GetMemberRolesByMemberId(id).ToList();
                return Ok(roleItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/memberroles/byroles
        [HttpGet("byroleids/{ids}")]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetMembersByRoleIds(string ids)
        {
            try
            {
                if (string.IsNullOrEmpty(ids)) return BadRequest();

                var memberItems = _service.GetMemberRolesByRoleIds(ids);

                return Ok(memberItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}