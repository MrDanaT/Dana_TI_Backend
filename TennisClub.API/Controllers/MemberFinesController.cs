using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.MemberFineServiceFolder;
using TennisClub.Common.MemberFine;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberFinesController : Controller
    {
        private readonly IMemberFineService _service;
        public MemberFinesController(IMemberFineService service)
        {
            _service = service;
        }

        // GET: api/memberfine
        [HttpGet]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetAllMemberFines()
        {
            IEnumerable<MemberFineReadDTO> memberFineItems = _service.GetAllMemberFines();

            return Ok(memberFineItems);
        }

        // GET: api/memberfine/5
        [HttpGet("{id}", Name = "GetMemberFineById")]
        public ActionResult<MemberFineReadDTO> GetMemberFineById(int id)
        {
            MemberFineReadDTO memberFine = _service.GetMemberFineById(id);

            if (memberFine == null)
            {
                return NotFound();
            }

            return Ok(memberFine);
        }

        // POST: api/memberfine
        [HttpPost]
        public ActionResult<MemberFineReadDTO> CreateMemberFine(MemberFineCreateDTO memberFineCreateDto)
        {
            MemberFineReadDTO createdMemberFine = _service.CreateMemberFine(memberFineCreateDto);


            return CreatedAtRoute(nameof(GetMemberFineById), new { createdMemberFine.Id }, createdMemberFine);
        }

        // PUT: api/memberfine/5
        [HttpPut("{id}")]
        public ActionResult UpdateMemberFine(int id, MemberFineUpdateDTO updateDTO)
        {
            MemberFineReadDTO memberFineModelFromRepo = _service.GetMemberFineById(id);

            if (memberFineModelFromRepo == null)
            {
                return NotFound();
            }

            _service.UpdateMemberFine(id, updateDTO);

            return NoContent();
        }

        // GET: api/memberfine/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetMemberFinesByMemberId(int id)
        {
            IEnumerable<MemberFineReadDTO> memberFineItems = _service.GetMemberFinesByMemberId(id);

            return Ok(memberFineItems);
        }
    }
}
