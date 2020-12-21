using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TennisClub.BL.GenderServiceFolder;
using TennisClub.Common.Gender;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IGenderService _service;

        public GendersController(IGenderService service)
        {
            _service = service;
        }

        // GET: api/genders
        [HttpGet]
        public ActionResult<IEnumerable<GenderReadDTO>> GetAllGenders()
        {
            return Ok(_service.GetAllGenders());
        }

        // GET: api/genders/5
        [HttpGet("{id}")]
        public ActionResult<GenderReadDTO> GetGenderById(int id)
        {
            var genderFromRepo = _service.GetGenderById(id);

            if (genderFromRepo == null) return NotFound();

            return Ok(genderFromRepo);
        }
    }
}