using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TennisClub.BL.GameResultServiceFolder;
using TennisClub.Common.GameResult;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : Controller
    {
        private readonly IGameResultService _service;

        public GameResultsController(IGameResultService service)
        {
            _service = service;
        }

        // GET: api/gameresults
        [HttpGet]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetAllGameResults(int? memberId, DateTime date)
        {
            var gameResultItems = _service.GetAllGameResults(memberId, date);

            return Ok(gameResultItems);
        }

        // GET: api/gameresults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public ActionResult<GameResultReadDTO> GetGameResultById(int id)
        {
            var gameResultItem = _service.GetGameResultById(id);

            if (gameResultItem == null) return NotFound();

            return Ok(gameResultItem);
        }

        // POST: api/gameresults
        [HttpPost]
        public ActionResult<GameResultReadDTO> CreateGameResult(GameResultCreateDTO gameResultCreateDto)
        {
            var createdGameResult = _service.CreateGameResult(gameResultCreateDto);

            return CreatedAtRoute(nameof(GetGameResultById), new {createdGameResult.Id}, createdGameResult);
        }

        // PUT: api/gameresults/5
        [HttpPut("{id}")]
        public ActionResult UpdateGameResult(int id, GameResultUpdateDTO updateDTO)
        {
            var gameResultModelFromRepo = _service.GetGameResultById(id);

            if (gameResultModelFromRepo == null) return NotFound();

            _service.UpdateGameResult(id, updateDTO);

            return NoContent();
        }
    }
}