using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.GenderRepository;
using TennisWebapplication.Repositories.LeagueRepository;

namespace TennisWebapplication.Controllers
{
    public class LeaguesController : Controller
    {
        private readonly ILeagueRepository _repo;

        public LeaguesController(ILeagueRepository repo)
        {
            _repo = repo;
        }

        public ActionResult<IEnumerable<League>> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _repo.GetAllLeagues();

            return Ok(leagueItems);
        }
    }
}
