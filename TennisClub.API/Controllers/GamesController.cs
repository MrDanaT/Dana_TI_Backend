using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.GameServiceFolder;
using TennisClub.Common.Game;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GamesController(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/games
        [HttpGet]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllGames()
        {
            IEnumerable<Game> gameItems = _service.GetAllGames();

            return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(gameItems));
        }

        // GET: api/games/5
        [HttpGet("{id}", Name = "GetGameById")]
        public ActionResult<GameReadDTO> GetGameById(int id)
        {
            Game gameItem = _service.GetGameById(id);

            if (gameItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameReadDTO>(gameItem));
        }

        // GET: api/games/futurebymemberid/5
        [HttpGet("futurebymemberid/{id}")]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllFutureGamesByMemberId(int id)
        {
            IEnumerable<Game> gameItems = _service.GetAllFutureGamesByMemberId(id);

            return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(gameItems));
        }

        // POST: api/games
        [HttpPost]
        public ActionResult<GameReadDTO> CreateGame(GameCreateDTO gameCreateDTO)
        {
            Game gameModel = _mapper.Map<Game>(gameCreateDTO);

            _service.CreateGame(gameModel);

            GameReadDTO gameReadDto = _mapper.Map<GameReadDTO>(gameModel);

            return CreatedAtRoute(nameof(GetGameById), new { gameReadDto.Id }, gameReadDto);
        }

        // PATCH: api/games/5
        [HttpPatch("{id}")]
        public ActionResult UpdateGame(int id, JsonPatchDocument<GameUpdateDTO> patchDoc)
        {
            Game gameModelFromRepo = _service.GetGameById(id);

            if (gameModelFromRepo == null)
            {
                return NotFound();
            }

            GameUpdateDTO gameToPatch = _mapper.Map<GameUpdateDTO>(gameModelFromRepo);
            patchDoc.ApplyTo(gameToPatch, ModelState);

            if (!TryValidateModel(gameToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(gameToPatch, gameModelFromRepo);

            _service.UpdateGame(gameModelFromRepo);

            return NoContent();
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            Game gameModelFromRepo = _service.GetGameById(id);

            if (gameModelFromRepo == null)
            {
                return NotFound();
            }

            _service.DeleteGame(gameModelFromRepo);

            return NoContent();
        }
    }
}

