using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.LeagueServiceFolder;
using TennisClub.Common.League;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService _service;
        private readonly IMapper _mapper;

        public LeaguesController(ILeagueService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/leagues
        [HttpGet]
        public ActionResult<IEnumerable<LeagueReadDTO>> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _service.GetAllLeagues();

            return Ok(_mapper.Map<IEnumerable<LeagueReadDTO>>(leagueItems));
        }

        // GET: api/leagues/5
        [HttpGet("{id}")]
        public ActionResult<LeagueReadDTO> GetLeagueById(byte id)
        {
            League leagueFromRepo = _service.GetLeagueById(id);

            if (leagueFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LeagueReadDTO>(leagueFromRepo));
        }
    }
}
