using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TennisClub.BL.GenderServiceFolder;
using TennisClub.Common;
using TennisClub.Common.Gender;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IGenderService _service;
        private readonly ILogger<GendersController> _logger;

        public GendersController(IGenderService service, ILogger<GendersController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/genders
        [HttpGet]
        public ActionResult<IEnumerable<GenderReadDTO>> GetAllGenders()
        {
            try
            {
                var genderItems = _service.GetAllGenders();
                return Ok(genderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/genders/5
        [HttpGet("{id}")]
        public ActionResult<GenderReadDTO> GetGenderById(int id)
        {
            try
            {
                var genderFromRepo = _service.GetGenderById(id);

                if (genderFromRepo.IsNull()) return NotFound();

                return Ok(genderFromRepo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}