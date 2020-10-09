using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.DAL.Repositories.GameResultRepository;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultsController : Controller
    {
        private readonly IGameResultRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepo;

        public GameResultsController(IGameResultRepository repo, IMapper mapper, IMemberRepository memberRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _memberRepo = memberRepo;
        }

        // GET: api/gameresults
        [HttpGet]
        public ActionResult<IEnumerable<GameResultReadDTO>> GetAllGameResults()
        {
            IEnumerable<GameResult> gameResultItems = _repo.GetAllGameResults();

            return Ok(_mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems));
        }

        // GET: api/gameresults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public ActionResult<GameResultReadDTO> GetGameResultById(int id)
        {
            GameResult gameResultItem = _repo.GetGameResultById(id);

            if (gameResultItem != null)
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

            _repo.Create(gameResultModel);
            _repo.SaveChanges();

            GameResultReadDTO gameResultReadDto = _mapper.Map<GameResultReadDTO>(gameResultModel);

            return CreatedAtRoute(nameof(GetGameResultById), new { gameResultReadDto.Id }, gameResultReadDto);
        }

        // PATCH: api/gameresults/5
        [HttpPatch("{id}")]
        public ActionResult PartialGameResultUpdate(int id, JsonPatchDocument<GameResultUpdateDTO> patchDoc)
        {
            GameResult gameResultModelFromRepo = _repo.GetGameResultById(id);

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

            _repo.Update(gameResultModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // GET: api/gameresults/bymemberid/5
        [HttpGet("bymemberid/{id}")]
        public ActionResult<GameResultReadDTO> GetGameResultsByMember(int id)
        {
            Member memberItem = _memberRepo.GetMemberById(id);
            IEnumerable<GameResult> gameResultItems = _repo.GetGameResultsByMember(memberItem);

            return Ok(_mapper.Map<GameResultReadDTO>(gameResultItems));
        }
    }
}
