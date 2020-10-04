using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.MemberRepository;
using TennisClub.DAL.Repositories.MemberRoleRepository;
using TennisClub.DTO.Member;
using TennisClub.DTO.MemberRole;
using TennisClub.DTO.Role;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRolesController : ControllerBase
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

        // GET: api/memberroles/getrolesbymember/5
        [HttpGet("getrolesbymember/{id}")]
        public ActionResult<IEnumerable<RoleReadDTO>> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            Member memberFromRepo = _memberRepo.GetMemberById(id);
            IEnumerable<Role> roleItems = _repo.GetRolesByMember(memberFromRepo).ToList();

            return Ok(_mapper.Map<IEnumerable<RoleReadDTO>>(roleItems));
        }

        // GET: api/memberroles/getmembersbyroles
        [HttpGet("getmembersbyroles")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetMembersByRoles(List<string> roles)
        {
            // TODO: Dit nakijken samen met repository.
            var memberItems = _repo.GetMembersByRoles(roles);

            return Ok(_mapper.Map<IEnumerable<MemberReadDTO>>(memberItems));
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name = "GetMemberRoleById")]
        public ActionResult<MemberRoleReadDTO> GetMemberRoleById(int id)
        {
            MemberRole memberRoleItem = _repo.GetMemberRoleById(id);

            if (memberRoleItem != null)
            {
                return Ok(_mapper.Map<MemberRoleReadDTO>(memberRoleItem));
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/memberroles
        [HttpPost]
        public ActionResult<MemberRoleReadDTO> CreateMemberRole(MemberRoleCreateDTO memberRoleCreateDTO)
        {
            MemberRole memberRoleModel = _mapper.Map<MemberRole>(memberRoleCreateDTO);

            _repo.CreateMemberRole(memberRoleModel);
            _repo.SaveChanges();

            MemberRoleReadDTO memberRoleReadDTO = _mapper.Map<MemberRoleReadDTO>(memberRoleModel);

            return CreatedAtRoute(nameof(GetRolesByMemberId), new { memberRoleReadDTO.Id }, memberRoleReadDTO);
        }

        // PATCH api/memberroles/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialMemberRoleUpdate(int id, JsonPatchDocument<MemberRoleUpdateDTO> patchDoc)
        {
            MemberRole memberRoleModelFromRepo = _repo.GetMemberRoleById(id);

            if (memberRoleModelFromRepo == null)
            {
                return NotFound();
            }

            MemberRoleUpdateDTO modelRoleToPatch = _mapper.Map<MemberRoleUpdateDTO>(memberRoleModelFromRepo);
            patchDoc.ApplyTo(modelRoleToPatch, ModelState);

            if (!TryValidateModel(modelRoleToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(modelRoleToPatch, memberRoleModelFromRepo);

            _repo.UpdateMemberRole(memberRoleModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}
