using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.MemberRepository;
using TennisClub.DAL.Repositories.MemberRoleRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRolesController : Controller
    {
        private readonly IMemberRoleRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepo;

        public MemberRolesController(IMemberRoleRepository repo, IMapper mapper, IMemberRepository memberRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _memberRepo = memberRepo;
        }

        // GET: api/memberroles
        [HttpGet]
        public ActionResult<IEnumerable<MemberRoleReadDTO>> GetAllMemberRoles()
        {
            IEnumerable<MemberRole> memberRoleItems = _repo.GetAll();

            return Ok(_mapper.Map<IEnumerable<MemberRoleReadDTO>>(memberRoleItems));
        }

        // GET api/memberroles/5
        [HttpGet("{id}", Name = "GetMemberRoleById")]
        public ActionResult<MemberRoleReadDTO> GetMemberRoleById(int id)
        {
            MemberRole memberRoleItem = _repo.GetById(id);

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

            _repo.Create(memberRoleModel);
            _repo.SaveChanges();

            MemberRoleReadDTO memberRoleReadDTO = _mapper.Map<MemberRoleReadDTO>(memberRoleModel);

            return CreatedAtRoute(nameof(GetMemberRoleById), new { memberRoleReadDTO.Id }, memberRoleReadDTO);
        }

        // PATCH api/memberroles/5
        [HttpPatch("{id}")]
        public ActionResult PartialMemberRoleUpdate(int id, JsonPatchDocument<MemberRoleUpdateDTO> patchDoc)
        {
            MemberRole memberRoleModelFromRepo = _repo.GetById(id);

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

            _repo.Update(memberRoleModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // GET: api/memberroles/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<RoleReadDTO>> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            Member memberFromRepo = _memberRepo.GetById(id);
            IEnumerable<Role> roleItems = _repo.GetRolesByMember(memberFromRepo).ToList();

            return Ok(_mapper.Map<IEnumerable<RoleReadDTO>>(roleItems));
        }

        // GET: api/memberroles/byroles
        [HttpGet("byroles")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetMembersByRoles(List<RoleCreateDTO> roleCreateDTOs)
        {
            // TODO: Dit nakijken samen met repository.
            var rolesFromRepo = _mapper.Map<List<Role>>(roleCreateDTOs);
            IEnumerable<Member> memberItems = _repo.GetMembersByRoles(rolesFromRepo);

            return Ok(_mapper.Map<IEnumerable<MemberReadDTO>>(memberItems));
        }
    }
}
