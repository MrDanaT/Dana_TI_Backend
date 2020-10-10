using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.Game;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.GameRepository;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : Controller
    {
        private readonly GameLogic _logic;
        private readonly IMapper _mapper;

        public GamesController(GameLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/games
        [HttpGet]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllGames()
        {
            IEnumerable<Game> gameItems = _logic.GetAllGames();

            return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(gameItems));
        }

        // GET: api/games/5
        [HttpGet("{id}", Name = "GetGameById")]
        public ActionResult<GameReadDTO> GetGameById(int id)
        {
            Game gameItem = _logic.GetGameById(id);

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
            var gameItems = _logic.GetAllFutureGamesByMemberId(id);

            return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(gameItems));
        }

        // POST: api/games
        [HttpPost]
        public ActionResult<GameReadDTO> CreateGame(GameCreateDTO gameCreateDTO)
        {
            Game gameModel = _mapper.Map<Game>(gameCreateDTO);

            _logic.CreateGame(gameModel);

            GameReadDTO gameReadDto = _mapper.Map<GameReadDTO>(gameModel);

            return CreatedAtRoute(nameof(GetGameById), new { gameReadDto.Id }, gameReadDto);
        }

        // PATCH: api/games/5
        [HttpPatch("{id}")]
        public ActionResult PartialGameUpdate(int id, JsonPatchDocument<GameUpdateDTO> patchDoc)
        {
            Game gameModelFromRepo = _logic.GetGameById(id);

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

            _logic.PartialGameUpdate(gameModelFromRepo);

            return NoContent();
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            Game gameModelFromRepo = _logic.GetGameById(id);

            if (gameModelFromRepo == null)
            {
                return NotFound();
            }

            _logic.DeleteGame(gameModelFromRepo);

            return NoContent();
        }
    }
}

