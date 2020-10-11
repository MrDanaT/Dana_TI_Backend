using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.MemberRoleServiceFolder;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRolesController : Controller
    {
        private readonly IMemberRoleService _service;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;

        public MemberRolesController(IMemberRoleService service, IMapper mapper, IMemberService memberService)
        {
            _service = service;
            _mapper = mapper;
            _memberService = memberService;
        }

        // GET: api/memberroles
        [HttpGet]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetAllMemberRoles()
        {
            IEnumerable<MemberRole> memberRoleItems = _service.GetAllMemberRoles();

            return Ok(_mapper.Map<IEnumerable<MemberRoleReadDTO>>(memberRoleItems));
        }

        // GET api/memberroles/5
        [HttpGet("{id}", Name = "GetMemberRoleById")]
        public ActionResult<MemberRoleReadDTO> GetMemberRoleById(int id)
        {
            MemberRole memberRoleItem = _service.GetMemberRoleById(id);

            if (memberRoleItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MemberRoleReadDTO>(memberRoleItem));
        }

        // POST: api/memberroles
        [HttpPost]
        public ActionResult<MemberRoleReadDTO> CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO)
        {
            MemberRole memberRoleModel = _mapper.Map<MemberRole>(memberRoleCreateDTO);

            _service.CreateMemberRole(memberRoleModel);

            MemberRoleReadDTO memberRoleReadDTO = _mapper.Map<MemberRoleReadDTO>(memberRoleModel);

            return CreatedAtRoute(nameof(GetMemberRoleById), new { memberRoleReadDTO.Id }, memberRoleReadDTO);
        }

        // PATCH api/memberroles/5
        [HttpPatch("{id}")]
        public ActionResult UpdateMemberRole(int id, JsonPatchDocument<MemberRoleUpdateDTO> patchDoc)
        {
            MemberRole memberRoleModelFromRepo = _service.GetMemberRoleById(id);

            if (memberRoleModelFromRepo == null)
            {
                return NotFound();
            }

            MemberRoleUpdateDTO memberRoleToPatch = _mapper.Map<MemberRoleUpdateDTO>(memberRoleModelFromRepo);
            patchDoc.ApplyTo(memberRoleToPatch, ModelState);

            if (!TryValidateModel(memberRoleToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(memberRoleToPatch, memberRoleModelFromRepo);

            _service.UpdateMemberRole(memberRoleModelFromRepo);

            return NoContent();
        }

        // GET: api/memberroles/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<RoleReadDTO>> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            IEnumerable<Role> roleItems = _service.GetRolesByMemberId(id).ToList();

            return Ok(_mapper.Map<IEnumerable<RoleReadDTO>>(roleItems));
        }

        // GET: api/memberroles/byroles
        [HttpGet("byroles")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetMembersByRoles(List<RoleReadDTO> roleCreateDTOs)
        {
            // TODO: Dit nakijken samen met repository.
            List<Role> rolesFromRepo = _mapper.Map<List<Role>>(roleCreateDTOs);
            IEnumerable<Member> memberItems = _service.GetMembersByRoles(rolesFromRepo);

            return Ok(_mapper.Map<IEnumerable<MemberReadDTO>>(memberItems));
        }
    }
}
