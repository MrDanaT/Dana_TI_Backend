using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.GenderRepository;
using TennisClub.DTO.Gender;

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
        public ActionResult<IEnumerable<Gender>> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _repo.GetAllGenders();

            return Ok(_mapper.Map<IEnumerable<GenderReadDTO>>(genderItems));
        }
    }
}
