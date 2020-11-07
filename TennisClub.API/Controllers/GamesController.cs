using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.GameServiceFolder;
using TennisClub.Common.Game;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        // GET: api/games
        [HttpGet]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllGames()
        {
            IEnumerable<GameReadDTO> gameItems = _service.GetAllGames();

            return Ok(gameItems);
        }

        // GET: api/games/5
        [HttpGet("{id}", Name = "GetGameById")]
        public ActionResult<GameReadDTO> GetGameById(int id)
        {
            GameReadDTO gameItem = _service.GetGameById(id);

            if (gameItem == null)
            {
                return NotFound();
            }

            return Ok(gameItem);
        }

        // GET: api/games/futurebymemberid/5
        [HttpGet("futurebymemberid/{id}")]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllFutureGamesByMemberId(int id)
        {
            IEnumerable<GameReadDTO> gameItems = _service.GetAllFutureGamesByMemberId(id);

            return Ok(gameItems);
        }

        // POST: api/games
        [HttpPost]
        public ActionResult<GameReadDTO> CreateGame(GameCreateDTO gameCreateDTO)
        {

            GameReadDTO createdGame = _service.CreateGame(gameCreateDTO);


            return CreatedAtRoute(nameof(GetGameById), new { createdGame.Id }, createdGame);
        }

        // PUT: api/games/5
        [HttpPut("{id}")]
        public ActionResult UpdateGame(int id, GameUpdateDTO updateDTO)
        {
            GameReadDTO gameModelFromRepo = _service.GetGameById(id);

            if (gameModelFromRepo == null)
            {
                return NotFound();
            }

            _service.UpdateGame(id, updateDTO);

            return NoContent();
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            GameReadDTO gameModelFromRepo = _service.GetGameById(id);

            if (gameModelFromRepo == null)
            {
                return NotFound();
            }

            _service.DeleteGame(id);

            return NoContent();
        }
    }
}

