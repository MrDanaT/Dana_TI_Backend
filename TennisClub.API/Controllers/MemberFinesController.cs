using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.MemberFineRepository;
using TennisClub.DAL.Repositories.MemberRepository;
using TennisClub.DTO.MemberFine;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberFinesController : Controller
    {
        private readonly IMemberFineRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepo;

        public MemberFinesController(IMemberFineRepository repo, IMapper mapper, IMemberRepository memberRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _memberRepo = memberRepo;
        }

        // GET: api/memberfine
        [HttpGet]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetAllMemberFines()
        {
            IEnumerable<MemberFine> memberFineItems = _repo.GetAllMemberFines();

            return Ok(_mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems));
        }

        // GET: api/memberfine/5
        [HttpGet("{id}", Name = "GetMemberFinesById")]
        public ActionResult<MemberFineReadDTO> GetMemberFinesById(int id)
        {
            MemberFine memberFine = _repo.GetMemberFinesById(id);

            if (memberFine == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MemberFineReadDTO>(memberFine));
        }

        // POST: api/memberfine
        [HttpPost]
        public ActionResult<MemberFineReadDTO> CreateMemberFine(MemberFineCreateDTO memberFineCreateDto)
        {
            MemberFine memberFineModel = _mapper.Map<MemberFine>(memberFineCreateDto);

            _repo.CreateMemberFine(memberFineModel);
            _repo.SaveChanges();

            MemberFineReadDTO memberFineReadDto = _mapper.Map<MemberFineReadDTO>(memberFineModel);

            return CreatedAtRoute(nameof(GetMemberFinesById), new { memberFineReadDto.Id }, memberFineReadDto);
        }

        // PATCH: api/memberfine/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialMemberFineUpdate(int id, JsonPatchDocument<MemberFineUpdateDTO> patchDoc)
        {
            MemberFine memberFineModelFromRepo = _repo.GetMemberFinesById(id);

            if (memberFineModelFromRepo == null)
            {
                return NotFound();
            }

            MemberFineUpdateDTO modelFineToPatch = _mapper.Map<MemberFineUpdateDTO>(memberFineModelFromRepo);
            patchDoc.ApplyTo(modelFineToPatch, ModelState);

            if (!TryValidateModel(modelFineToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(modelFineToPatch, memberFineModelFromRepo);

            _repo.UpdateMemberFine(memberFineModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // GET: api/memberfine/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetMemberFinesByMemberId(int id)
        {
            Member memberFromRepo = _memberRepo.GetMemberById(id);
            IEnumerable<MemberFine> memberFineItems = _repo.GetMemberFinesByMember(memberFromRepo);

            return Ok(_mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems));
        }
    }
}
