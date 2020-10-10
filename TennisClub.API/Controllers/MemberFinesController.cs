using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberFinesController : Controller
    {
        private readonly MemberFineLogic _logic;
        private readonly IMapper _mapper;

        public MemberFinesController(MemberFineLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/memberfine
        [HttpGet]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetAllMemberFines()
        {
            IEnumerable<MemberFine> memberFineItems = _logic.GetAllMemberFines();

            return Ok(_mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems));
        }

        // GET: api/memberfine/5
        [HttpGet("{id}", Name = "GetMemberFineById")]
        public ActionResult<MemberFineReadDTO> GetMemberFineById(int id)
        {
            MemberFine memberFine = _logic.GetMemberFineById(id);

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

            _logic.CreateMemberFine(memberFineModel);

            MemberFineReadDTO memberFineReadDto = _mapper.Map<MemberFineReadDTO>(memberFineModel);

            return CreatedAtRoute(nameof(GetMemberFineById), new { memberFineReadDto.Id }, memberFineReadDto);
        }

        // PATCH: api/memberfine/5
        [HttpPatch("{id}")]
        public ActionResult PartialMemberFineUpdate(int id, JsonPatchDocument<MemberFineUpdateDTO> patchDoc)
        {
            MemberFine memberFineModelFromRepo = _logic.GetMemberFineById(id);

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

            _logic.PartialMemberFineUpdate(memberFineModelFromRepo);

            return NoContent();
        }

        // GET: api/memberfine/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetMemberFinesByMemberId(int id)
        {
            IEnumerable<MemberFine> memberFineItems = _logic.GetMemberFinesByMemberId(id);

            return Ok(_mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems));
        }
    }
}
