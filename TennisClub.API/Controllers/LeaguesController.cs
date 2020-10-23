using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.LeagueServiceFolder;
using TennisClub.Common.League;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService _service;

        public LeaguesController(ILeagueService service)
        {
            _service = service;
        }

        // GET: api/leagues
        [HttpGet]
        public ActionResult<IEnumerable<LeagueReadDTO>> GetAllLeagues()
        {
            IEnumerable<LeagueReadDTO> leagueItems = _service.GetAllLeagues();

            return Ok(leagueItems);
        }

        // GET: api/leagues/5
        [HttpGet("{id}")]
        public ActionResult<LeagueReadDTO> GetLeagueById(int id)
        {
            LeagueReadDTO leagueFromRepo = _service.GetLeagueById(id);

            if (leagueFromRepo == null)
            {
                return NotFound();
            }

            return Ok(leagueFromRepo);
        }
    }
}
