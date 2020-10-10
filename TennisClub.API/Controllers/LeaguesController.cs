using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.LeagueRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : Controller
    {
        private readonly LeagueLogic _logic;
        private readonly IMapper _mapper;

        public LeaguesController(LeagueLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/leagues
        [HttpGet]
        public ActionResult<IEnumerable<LeagueReadDTO>> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _logic.GetAllLeagues();

            return Ok(_mapper.Map<IEnumerable<LeagueReadDTO>>(leagueItems));
        }

        // GET: api/leagues/5
        [HttpGet("{id}")]
        public ActionResult<LeagueReadDTO> GetLeagueById(int id)
        {
            League leagueFromRepo = _logic.GetLeagueById(id);

            if (leagueFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LeagueReadDTO>(leagueFromRepo));
        }
    }
}
