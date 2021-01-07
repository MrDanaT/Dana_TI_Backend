using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TennisClub.BL.MemberFineServiceFolder;
using TennisClub.Common;
using TennisClub.Common.MemberFine;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberFinesController : Controller
    {
        private readonly IMemberFineService _service;
        private readonly ILogger<MemberFinesController> _logger;

        public MemberFinesController(IMemberFineService service, ILogger<MemberFinesController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/memberfine
        [HttpGet]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetAllMemberFines()
        {
            try
            {
                var memberFineItems = _service.GetAllMemberFines();
                return Ok(memberFineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/memberfine/5
        [HttpGet("{id}", Name = "GetMemberFineById")]
        public ActionResult<MemberFineReadDTO> GetMemberFineById(int id)
        {
            try
            {
                var memberFine = _service.GetMemberFineById(id);

                if (memberFine.IsNull()) return NotFound();

                return Ok(memberFine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/memberfine
        [HttpPost]
        public ActionResult<MemberFineReadDTO> CreateMemberFine(MemberFineCreateDTO memberFineCreateDto)
        {
            try
            {
                var createdMemberFine = _service.CreateMemberFine(memberFineCreateDto);
                return CreatedAtRoute(nameof(GetMemberFineById), new {createdMemberFine.Id}, createdMemberFine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // PUT: api/memberfine/5
        [HttpPut("{id}")]
        public ActionResult UpdateMemberFine(int id, MemberFineUpdateDTO updateDTO)
        {
            try
            {
                var memberFineModelFromRepo = _service.GetMemberFineById(id);

                if (memberFineModelFromRepo.IsNull()) return NotFound();

                _service.UpdateMemberFine(id, updateDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/memberfine/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<MemberFineReadDTO>> GetMemberFinesByMemberId(int id)
        {
            try
            {
                var memberFineItems = _service.GetMemberFinesByMemberId(id);
                return Ok(memberFineItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}