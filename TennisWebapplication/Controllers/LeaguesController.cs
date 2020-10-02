using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.LeagueRepository;

namespace TennisWebapplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : Controller
    {
        private readonly ILeagueRepository _repo;

        public LeaguesController(ILeagueRepository repo)
        {
            _repo = repo;
        }

        // GET: api/leagues
        [HttpGet]
        public ActionResult<IEnumerable<League>> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _repo.GetAllLeagues();

            return Ok(leagueItems);
        }
    }
}
