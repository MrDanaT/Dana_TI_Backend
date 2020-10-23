using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<GameResultReadDTO>> GetAllGameResults()
        {
            IEnumerable<GameResultReadDTO> gameResultItems = _service.GetAllGameResults();

            return Ok(gameResultItems);
        }

        // GET: api/gameresults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public ActionResult<GameResultReadDTO> GetGameResultById(int id)
        {
            GameResultReadDTO gameResultItem = _service.GetGameResultById(id);

            if (gameResultItem == null)
            {
                return NotFound();
            }

            return Ok(gameResultItem);
        }

        // POST: api/gameresults
        [HttpPost]
        public ActionResult<GameResultReadDTO> CreateGameResult(GameResultCreateDTO gameResultCreateDto)
        {
            GameResultReadDTO createdGameResult = _service.CreateGameResult(gameResultCreateDto);

            return CreatedAtRoute(nameof(GetGameResultById), new { createdGameResult.Id }, createdGameResult);
        }

        // PATCH: api/gameresults/5
        [HttpPatch("{id}")]
        public ActionResult UpdateGameResult(int id, JsonPatchDocument<GameResultUpdateDTO> patchDoc)
        {
            GameResultReadDTO gameResultModelFromRepo = _service.GetGameResultById(id);

            if (gameResultModelFromRepo == null)
            {
                return NotFound();
            }

            GameResultUpdateDTO gameResultToPatch = _service.GetUpdateDTOByReadDTO(gameResultModelFromRepo);
            patchDoc.ApplyTo(gameResultToPatch, ModelState);

            if (!TryValidateModel(gameResultToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _service.UpdateGameResult(gameResultToPatch, gameResultModelFromRepo);

            return NoContent();
        }

        // GET: api/gameresults/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetGameResultsByMember(int id)
        {
            IEnumerable<GameResultReadDTO> gameResultItems = _service.GetGameResultsByMember(id);

            return Ok(gameResultItems);
        }
    }
}
