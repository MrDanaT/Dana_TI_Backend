using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TennisClub.BL.LeagueServiceFolder;
using TennisClub.Common;
using TennisClub.Common.League;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService _service;
        private readonly ILogger<LeaguesController> _logger;

        public LeaguesController(ILeagueService service, ILogger<LeaguesController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/leagues
        [HttpGet]
        public ActionResult<IEnumerable<LeagueReadDTO>> GetAllLeagues()
        {
            try
            {
                var leagueItems = _service.GetAllLeagues();
                return Ok(leagueItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/leagues/5
        [HttpGet("{id}")]
        public ActionResult<LeagueReadDTO> GetLeagueById(int id)
        {
            try
            {
                var leagueFromRepo = _service.GetLeagueById(id);

                if (leagueFromRepo.IsNull()) return NotFound();

                return Ok(leagueFromRepo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}