using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL;
using TennisClub.Common.GameResult;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : Controller
    {
        private readonly GameResultLogic _logic;
        private readonly IMapper _mapper;

        public GameResultsController(GameResultLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: api/gameresults
        [HttpGet]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetAllGameResults()
        {
            IEnumerable<GameResult> gameResultItems = _logic.GetAllGameResults();

            return Ok(_mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems));
        }

        // GET: api/gameresults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public ActionResult<GameResultReadDTO> GetGameResultById(int id)
        {
            GameResult gameResultItem = _logic.GetGameResultById(id);

            if (gameResultItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameResultReadDTO>(gameResultItem));
        }

        // POST: api/gameresults
        [HttpPost]
        public ActionResult<GameResultReadDTO> CreateGameResult(GameResultCreateDTO gameResultCreateDto)
        {
            GameResult gameResultModel = _mapper.Map<GameResult>(gameResultCreateDto);

            _logic.CreateGameResult(gameResultModel);

            GameResultReadDTO gameResultReadDto = _mapper.Map<GameResultReadDTO>(gameResultModel);

            return CreatedAtRoute(nameof(GetGameResultById), new { gameResultReadDto.Id }, gameResultReadDto);
        }

        // PATCH: api/gameresults/5
        [HttpPatch("{id}")]
        public ActionResult PartialGameResultUpdate(int id, JsonPatchDocument<GameResultUpdateDTO> patchDoc)
        {
            GameResult gameResultModelFromRepo = _logic.GetGameResultById(id);

            if (gameResultModelFromRepo == null)
            {
                return NotFound();
            }

            GameResultUpdateDTO gameResultToPatch = _mapper.Map<GameResultUpdateDTO>(gameResultModelFromRepo);
            patchDoc.ApplyTo(gameResultToPatch, ModelState);

            if (!TryValidateModel(gameResultToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(gameResultToPatch, gameResultModelFromRepo);

            _logic.PartialGameResultUpdate(gameResultModelFromRepo);

            return NoContent();
        }

        // GET: api/gameresults/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetGameResultsByMember(int id)
        {
            IEnumerable<GameResult> gameResultItems = _logic.GetGameResultsByMember(id);

            return Ok(_mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems));
        }
    }
}
