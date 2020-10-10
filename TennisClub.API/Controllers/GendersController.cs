using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.GenderRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : Controller
    {
        private readonly GenderLogic _logic;
        private readonly IMapper _mapper;

        public GendersController(GenderLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/genders
        [HttpGet]
        public ActionResult<IEnumerable<GenderReadDTO>> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _logic.GetAllGenders();

            return Ok(_mapper.Map<IEnumerable<GenderReadDTO>>(genderItems));
        }

        // GET: api/genders/5
        [HttpGet("{id}")]
        public ActionResult<GenderReadDTO> GetGenderById(int id)
        {
            Gender genderFromRepo = _logic.GetGenderById(id);

            if (genderFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenderReadDTO>(genderFromRepo));
        }
    }
}
