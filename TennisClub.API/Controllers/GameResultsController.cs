using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.GameResultServiceFolder;
using TennisClub.Common.GameResult;
using TennisClub.DAL.Entities;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : Controller
    {
        private readonly IGameResultService _service;
        private readonly IMapper _mapper;

        public GameResultsController(IGameResultService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/gameresults
        [HttpGet]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetAllGameResults()
        {
            IEnumerable<GameResult> gameResultItems = _service.GetAllGameResults();

            return Ok(_mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems));
        }

        // GET: api/gameresults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public ActionResult<GameResultReadDTO> GetGameResultById(int id)
        {
            GameResult gameResultItem = _service.GetGameResultById(id);

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

            _service.CreateGameResult(gameResultModel);

            GameResultReadDTO gameResultReadDto = _mapper.Map<GameResultReadDTO>(gameResultModel);

            return CreatedAtRoute(nameof(GetGameResultById), new { gameResultReadDto.Id }, gameResultReadDto);
        }

        // PATCH: api/gameresults/5
        [HttpPatch("{id}")]
        public ActionResult UpdateGameResult(int id, JsonPatchDocument<GameResultUpdateDTO> patchDoc)
        {
            GameResult gameResultModelFromRepo = _service.GetGameResultById(id);

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

            _service.UpdateGameResult(gameResultModelFromRepo);

            return NoContent();
        }

        // GET: api/gameresults/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetGameResultsByMember(int id)
        {
            IEnumerable<GameResult> gameResultItems = _service.GetGameResultsByMember(id);

            return Ok(_mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems));
        }
    }
}
