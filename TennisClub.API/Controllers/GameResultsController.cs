using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TennisClub.BL.GameResultServiceFolder;
using TennisClub.Common;
using TennisClub.Common.GameResult;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : Controller
    {
        private readonly IGameResultService _service;
        private readonly ILogger<GameResultsController> _logger;

        public GameResultsController(IGameResultService service, ILogger<GameResultsController> logger)
        {
            _service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/gameresults
        [HttpGet]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetAllGameResults(int? memberId, DateTime date)
        {
            try
            {
                var gameResultItems = _service.GetAllGameResults(memberId, date);
                return Ok(gameResultItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // GET: api/gameresults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public ActionResult<GameResultReadDTO> GetGameResultById(int id)
        {
            try
            {
                var gameResultItem = _service.GetGameResultById(id);

                if (gameResultItem.IsNull()) return NotFound();

                return Ok(gameResultItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // POST: api/gameresults
        [HttpPost]
        public ActionResult<GameResultReadDTO> CreateGameResult(GameResultCreateDTO gameResultCreateDto)
        {
            try
            {
                var createdGameResult = _service.CreateGameResult(gameResultCreateDto);
                return CreatedAtRoute(nameof(GetGameResultById), new {createdGameResult.Id}, createdGameResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        // PUT: api/gameresults/5
        [HttpPut("{id}")]
        public ActionResult UpdateGameResult(int id, GameResultUpdateDTO updateDTO)
        {
            try
            {
                var gameResultModelFromRepo = _service.GetGameResultById(id);

                if (gameResultModelFromRepo.IsNull()) return NotFound();

                _service.UpdateGameResult(id, updateDTO);

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