using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.LeagueRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : Controller
    {
        private readonly ILeagueRepository _repo;
        private readonly IMapper _mapper;

        public LeaguesController(ILeagueRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/leagues
        [HttpGet]
        public ActionResult<IEnumerable<LeagueReadDTO>> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _repo.GetAll();

            return Ok(_mapper.Map<IEnumerable<LeagueReadDTO>>(leagueItems));
        }

        // GET: api/leagues/5
        [HttpGet("{id}")]
        public ActionResult<LeagueReadDTO> GetGenderById(int id)
        {
            League leagueFromRepo = _repo.GetById(id);

            if (leagueFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LeagueReadDTO>(leagueFromRepo));
        }
    }
}
