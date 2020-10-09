using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.GenderRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IGenderRepository _repo;
        private readonly IMapper _mapper;

        public GendersController(IGenderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/genders
        [HttpGet]
        public ActionResult<IEnumerable<GenderReadDTO>> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _repo.GetAll();

            return Ok(_mapper.Map<IEnumerable<GenderReadDTO>>(genderItems));
        }

        // GET: api/genders/5
        [HttpGet("{id}")]
        public ActionResult<GenderReadDTO> GetGenderById(int id)
        {
            Gender genderFromRepo = _repo.GetById(id);

            if (genderFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenderReadDTO>(genderFromRepo));
        }
    }
}
