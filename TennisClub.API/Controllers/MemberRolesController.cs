using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TennisClub.BL.MemberRoleServiceFolder;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRolesController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMemberRoleService _service;

        public MemberRolesController(IMemberRoleService service, IMemberService memberService)
        {
            _service = service;
            _memberService = memberService;
        }

        // GET: api/memberroles
        [HttpGet]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetAllMemberRoles()
        {
            var memberRoleItems = _service.GetAllMemberRoles();

            return Ok(memberRoleItems);
        }

        // GET api/memberroles/5
        [HttpGet("{id}", Name = "GetMemberRoleById")]
        public ActionResult<MemberRoleReadDTO> GetMemberRoleById(int id)
        {
            var memberRoleItem = _service.GetMemberRoleById(id);

            if (memberRoleItem == null) return NotFound();

            return Ok(memberRoleItem);
        }

        // POST: api/memberroles
        [HttpPost]
        public ActionResult<MemberRoleReadDTO> CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO)
        {
            var createdMemberRole = _service.CreateMemberRole(memberRoleCreateDTO);

            return CreatedAtRoute(nameof(GetMemberRoleById), new {createdMemberRole.Id}, createdMemberRole);
        }

        // PATCH api/memberroles/5
        [HttpPut("{id}")]
        public ActionResult UpdateMemberRole(int id, MemberRoleUpdateDTO updateDTO)
        {
            var memberRoleModelFromRepo = _service.GetMemberRoleById(id);

            if (memberRoleModelFromRepo == null) return NotFound();

            _service.UpdateMemberRole(id, updateDTO);

            return NoContent();
        }

        // GET: api/memberroles/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetRolesByMemberId(int id)
        {
            IEnumerable<MemberRoleReadDTO> roleItems = _service.GetMemberRolesByMemberId(id).ToList();

            return Ok(roleItems);
        }

        // GET: api/memberroles/byroles
        [HttpGet("byroleids/{ids}")]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetMembersByRoleIds(string ids)
        {
            var intIds = ids.Split(',').Select(int.Parse).ToArray();
            var memberItems = _service.GetMemberRolesByRoleIds(intIds);

            return Ok(memberItems);
        }
    }
}