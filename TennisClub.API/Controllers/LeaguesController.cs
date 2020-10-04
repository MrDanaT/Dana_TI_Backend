using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.LeagueRepository;
using TennisClub.DTO.League;

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
        public ActionResult<IEnumerable<League>> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _repo.GetAllLeagues();

            return Ok(_mapper.Map<IEnumerable<LeagueReadDTO>>(leagueItems));
        }
    }
}
