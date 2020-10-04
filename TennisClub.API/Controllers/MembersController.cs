using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.MemberRepository;
using TennisClub.DTO.Member;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _repo;
        private readonly IMapper _mapper;

        public MembersController(IMemberRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/members
        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllMembers()
        {
            IEnumerable<Member> memberItems = _repo.GetAllMembers();

            return Ok(_mapper.Map<IEnumerable<MemberReadDTO>>(memberItems));
        }

        // GET: api/members/5
        [HttpGet("{id}", Name = "GetMemberById")]
        public ActionResult<MemberReadDTO> GetMemberById(int id)
        {
            Member memberFromRepo = _repo.GetMemberById(id);

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

            _repo.CreateMember(memberModel);
            _repo.SaveChanges();

            MemberReadDTO memberReadDTO = _mapper.Map<MemberReadDTO>(memberModel);

            return CreatedAtRoute(nameof(GetMemberById), new { memberReadDTO.Id }, memberReadDTO);
        }
        // PATCH: api/members/5
        [HttpPatch("{id}")]
        public ActionResult PartialMemberUpdate(int id, JsonPatchDocument<MemberUpdateDTO> patchDoc)
        {
            Member memberModelFromRepo = _repo.GetMemberById(id);

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

            _repo.UpdateMember(memberModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }
        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMember(int id)
        {
            Member memberFromRepo = _repo.GetMemberById(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }

            _repo.DeleteMember(memberFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }


    }
}
