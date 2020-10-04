using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisClub.BL.Entities;
using TennisClub.DAL;
using TennisClub.DAL.Repositories.MemberRepository;

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
        public ActionResult<IEnumerable<Member>> GetAllMembers()
        {
            return Ok(_repo.GetAllMembers());
        }

        // GET: api/members/5
        [HttpGet("{id}", Name = "GetMemberById")]
        public ActionResult<IEnumerable<Member>> GetMemberById(int id)
        {
            Member memberFromRepo = _repo.GetMemberById(id);

            if (memberFromRepo == null)
            {
                return NotFound();
            }

            return Ok(memberFromRepo);
        }

        // POST: api/members
        [HttpPost]
        public ActionResult<Member> CreateMember(Member member)
        {
            _repo.CreateMember(member);
            _repo.SaveChanges();

            return CreatedAtRoute(nameof(GetMemberById), new { member.Id }, member);
        }

        // DELETE: api/members/5
        [HttpDelete("{id")]
        public ActionResult<Member> DeleteMember(int id)
        {
            Member memberFromRepo = _repo.GetMemberById(id);

            if (memberFromRepo == null)
                return NotFound();

            _repo.DeleteMember(memberFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // PATH: api/members/5
        [HttpPatch("{id")]
        public ActionResult<Member> PartialMemberUpdate(int id, JsonPatchDocument<Member> patchDoc)
        {
            Member memberFromRepo = _repo.GetMemberById(id);

            if (memberFromRepo == null)
                return NotFound();

            patchDoc.ApplyTo(memberFromRepo, ModelState);

            if (!TryValidateModel(memberFromRepo))
            {
                return ValidationProblem(ModelState);
            }

            _repo.UpdateMember(memberFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}
