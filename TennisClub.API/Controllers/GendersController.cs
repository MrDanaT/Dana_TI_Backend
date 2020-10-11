using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.GenderServiceFolder;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IGenderService _service;
        private readonly IMapper _mapper;

        public GendersController(IGenderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/genders
        [HttpGet]
        public ActionResult<IEnumerable<GenderReadDTO>> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _service.GetAllGenders();

            return Ok(_mapper.Map<IEnumerable<GenderReadDTO>>(genderItems));
        }

        // GET: api/genders/5
        [HttpGet("{id}")]
        public ActionResult<GenderReadDTO> GetGenderById(byte id)
        {
            Gender genderFromRepo = _service.GetGenderById(id);

            if (genderFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenderReadDTO>(genderFromRepo));
        }
    }
}
