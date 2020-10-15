using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.Common.Member;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : Controller
    {
        private readonly IMemberService _service;

        public MembersController(IMemberService service)
        {
            _service = service;
        }

        // GET: api/members
        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllMembers()
        {
            IEnumerable<MemberReadDTO> memberItems = _service.GetAllMembers();

            return Ok(memberItems);
        }

        // GET: api/members/5
        [HttpGet("{id}", Name = "GetMemberById")]
        public ActionResult<MemberReadDTO> GetMemberById(int id)
        {
            MemberReadDTO memberFromRepo = _service.GetMemberById(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }

            return Ok(memberFromRepo);
        }

        // POST: api/members
        [HttpPost]
        public ActionResult<MemberReadDTO> CreateMember(MemberCreateDTO memberCreateDTO)
        {
            MemberReadDTO memberReadDTO = _service.CreateMember(memberCreateDTO);

            return CreatedAtRoute(nameof(GetMemberById), new { memberReadDTO.Id }, memberReadDTO);
        }
        // PATCH: api/members/5
        [HttpPatch("{id}")]
        public ActionResult UpdateMember(int id, JsonPatchDocument<MemberUpdateDTO> patchDoc)
        {
            MemberReadDTO memberModelFromRepo = _service.GetMemberById(id);

            if (memberModelFromRepo == null)
            {
                return NotFound();
            }

            MemberUpdateDTO memberToPatch = _service.GetUpdateDTOByReadDTO(memberModelFromRepo);
            patchDoc.ApplyTo(memberToPatch, ModelState);

            if (!TryValidateModel(memberModelFromRepo))
            {
                return ValidationProblem(ModelState);
            }

            _service.UpdateMember(memberToPatch, memberModelFromRepo);

            return NoContent();
        }
        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMember(int id)
        {
            MemberReadDTO memberFromRepo = _service.GetMemberById(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }

            _service.DeleteMember(memberFromRepo);

            return NoContent();
        }

        // GET: api/members/active
        [HttpGet("active")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllActiveMembers()
        {
            IEnumerable<MemberReadDTO> memberItems = _service.GetAllActiveMembers();

            return Ok(memberItems);
        }
    }
}
