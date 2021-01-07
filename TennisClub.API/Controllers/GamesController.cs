using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TennisClub.BL.GameServiceFolder;
using TennisClub.Common;
using TennisClub.Common.Game;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService _service;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService service, ILogger<GamesController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/games
        [HttpGet]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllGames()
        {
            try
            {
                var gameItems = _service.GetAllGames();
                return Ok(gameItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/games/5
        [HttpGet("{id}", Name = "GetGameById")]
        public ActionResult<GameReadDTO> GetGameById(int id)
        {
            try
            {
                var gameItem = _service.GetGameById(id);

                if (gameItem.IsNull()) return NotFound();

                return Ok(gameItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/games/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<GameReadDTO>> GetGamesByMemberId(int id)
        {
            try
            {
                var gameItems = _service.GetGamesByMemberId(id);
                return Ok(gameItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/games
        [HttpPost]
        public ActionResult<GameReadDTO> CreateGame(GameCreateDTO gameCreateDTO)
        {
            try
            {
                var createdGame = _service.CreateGame(gameCreateDTO);

                if (createdGame.IsNull()) return BadRequest();

                return CreatedAtRoute(nameof(GetGameById), new {createdGame.Id}, createdGame);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // PUT: api/games/5
        [HttpPut("{id}")]
        public ActionResult UpdateGame(int id, GameUpdateDTO updateDTO)
        {
            try
            {
                var gameModelFromRepo = _service.GetGameById(id);

                if (gameModelFromRepo.IsNull()) return NotFound();

                _service.UpdateGame(id, updateDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            try
            {
                var gameModelFromRepo = _service.GetGameById(id);

                if (gameModelFromRepo.IsNull()) return NotFound();

                _service.DeleteGame(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}