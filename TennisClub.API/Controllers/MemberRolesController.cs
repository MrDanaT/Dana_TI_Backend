using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.MemberRoleServiceFolder;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRolesController : Controller
    {
        private readonly IMemberRoleService _service;
        private readonly IMemberService _memberService;

        public MemberRolesController(IMemberRoleService service, IMemberService memberService)
        {
            _service = service;
            _memberService = memberService;
        }

        // GET: api/memberroles
        [HttpGet]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetAllMemberRoles()
        {
            IEnumerable<MemberRoleReadDTO> memberRoleItems = _service.GetAllMemberRoles();

            return Ok(memberRoleItems);
        }

        // GET api/memberroles/5
        [HttpGet("{id}", Name = "GetMemberRoleById")]
        public ActionResult<MemberRoleReadDTO> GetMemberRoleById(int id)
        {
            MemberRoleReadDTO memberRoleItem = _service.GetMemberRoleById(id);

            if (memberRoleItem == null)
            {
                return NotFound();
            }

            return Ok(memberRoleItem);
        }

        // POST: api/memberroles
        [HttpPost]
        public ActionResult<MemberRoleReadDTO> CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO)
        {
            MemberRoleReadDTO createdMemberRole = _service.CreateMemberRole(memberRoleCreateDTO);

            return CreatedAtRoute(nameof(GetMemberRoleById), new { createdMemberRole.Id }, createdMemberRole);
        }

        // PATCH api/memberroles/5
        [HttpPatch("{id}")]
        public ActionResult UpdateMemberRole(int id, JsonPatchDocument<MemberRoleUpdateDTO> patchDoc)
        {
            MemberRoleReadDTO memberRoleModelFromRepo = _service.GetMemberRoleById(id);

            if (memberRoleModelFromRepo == null)
            {
                return NotFound();
            }

            MemberRoleUpdateDTO memberRoleToPatch = _service.GetUpdateDTOByReadDTO(memberRoleModelFromRepo);
            patchDoc.ApplyTo(memberRoleToPatch, ModelState);

            if (!TryValidateModel(memberRoleToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _service.UpdateMemberRole(memberRoleToPatch, memberRoleModelFromRepo);

            return NoContent();
        }

        // GET: api/memberroles/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<RoleReadDTO>> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            IEnumerable<RoleReadDTO> roleItems = _service.GetRolesByMemberId(id).ToList();

            return Ok(roleItems);
        }

        // GET: api/memberroles/byroles
        [HttpGet("byroles")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetMembersByRoles(List<RoleReadDTO> roleReadDTOs)
        {
            // TODO: Dit nakijken samen met repository.
            IEnumerable<MemberReadDTO> memberItems = _service.GetMembersByRoles(roleReadDTOs);

            return Ok(memberItems);
        }
    }
}
