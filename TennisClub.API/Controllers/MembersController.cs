using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TennisClub.BL.MemberServiceFolder;
using TennisClub.Common;
using TennisClub.Common.Member;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : Controller
    {
        private readonly IMemberService _service;
        private readonly ILogger<MembersController> _logger;

        public MembersController(IMemberService service, ILogger<MembersController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/members
        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllMembers(
            string? federationNr = "",
            string? firstName = "",
            string? lastName = "",
            string? location = "")
        {
            try
            {
                var memberItems = _service.GetAllMembers(federationNr, firstName, lastName, location);
                return Ok(memberItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/members/active/speler
        [HttpGet("active/speler")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllActiveSpelerMembers()
        {
            try
            {
                var memberItems = _service.GetAllActiveSpelerMembers();
                return Ok(memberItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/members/5
        [HttpGet("{id}", Name = "GetMemberById")]
        public ActionResult<MemberReadDTO> GetMemberById(int id)
        {
            try
            {
                var memberFromRepo = _service.GetMemberById(id);

                if (memberFromRepo.IsNull()) return NotFound();

                return Ok(memberFromRepo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/members
        [HttpPost]
        public ActionResult<MemberReadDTO> CreateMember(MemberCreateDTO memberCreateDTO)
        {
            try
            {
                var memberReadDTO = _service.CreateMember(memberCreateDTO);
                return CreatedAtRoute(nameof(GetMemberById), new {memberReadDTO.Id}, memberReadDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // PUT: api/members/5
        [HttpPut("{id}")]
        public ActionResult UpdateMember(int id, MemberUpdateDTO updateDTO)
        {
            try
            {
                var memberModelFromRepo = _service.GetMemberById(id);

                if (memberModelFromRepo.IsNull()) return NotFound();

                _service.UpdateMember(id, updateDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMember(int id)
        {
            try
            {
                var memberFromRepo = _service.GetMemberById(id);

                if (memberFromRepo.IsNull()) return NotFound();

                _service.DeleteMember(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/members/active
        [HttpGet("active")]
        public ActionResult<IEnumerable<MemberReadDTO>> GetAllActiveMembers(
            string? federationNr = "",
            string? firstName = "",
            string? lastName = "",
            string? location = "")
        {
            try
            {
                var memberItems = _service.GetAllActiveMembers(federationNr, firstName, lastName, location);
                return Ok(memberItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}