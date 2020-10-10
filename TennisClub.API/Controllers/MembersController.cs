using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : Controller
    {
        private readonly MemberLogic _logic;
        private readonly IMapper _mapper;

        public MembersController(MemberLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/members
        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllMembers()
        {
            IEnumerable<Member> memberItems = _logic.GetAllMembers();

            return Ok(_mapper.Map<IEnumerable<MemberReadDTO>>(memberItems));
        }

        // GET: api/members/5
        [HttpGet("{id}", Name = "GetMemberById")]
        public ActionResult<MemberReadDTO> GetMemberById(int id)
        {
            Member memberFromRepo = _logic.GetMemberById(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MemberReadDTO>(memberFromRepo));
        }

        // POST: api/members
        [HttpPost]
        public ActionResult<MemberReadDTO> CreateMember(MemberCreateDTO memberCreateDTO)
        {
            Member memberModel = _mapper.Map<Member>(memberCreateDTO);

            _logic.CreateMember(memberModel);

            MemberReadDTO memberReadDTO = _mapper.Map<MemberReadDTO>(memberModel);

            return CreatedAtRoute(nameof(GetMemberById), new { memberReadDTO.Id }, memberReadDTO);
        }
        // PATCH: api/members/5
        [HttpPatch("{id}")]
        public ActionResult PartialMemberUpdate(int id, JsonPatchDocument<MemberUpdateDTO> patchDoc)
        {
            Member memberModelFromRepo = _logic.GetMemberById(id);

            if (memberModelFromRepo == null)
            {
                return NotFound();
            }

            MemberUpdateDTO memberToPatch = _mapper.Map<MemberUpdateDTO>(memberModelFromRepo);
            patchDoc.ApplyTo(memberToPatch, ModelState);

            if (!TryValidateModel(memberModelFromRepo))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(memberToPatch, memberModelFromRepo);

            _logic.PartialMemberUpdate(memberModelFromRepo);

            return NoContent();
        }
        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMember(int id)
        {
            Member memberFromRepo = _logic.GetMemberById(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }

            _logic.DeleteMember(memberFromRepo);

            return NoContent();
        }

        // GET: api/members/active
        [HttpGet("active")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllActiveMembers()
        {
            IEnumerable<Member> memberItems = _logic.GetAllActiveMembers();

            return Ok(_mapper.Map<IEnumerable<MemberReadDTO>>(memberItems));
        }
    }
}
