using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly IGameRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepo;

        public GamesController(IGameRepository repo, IMapper mapper, IMemberRepository memberRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _memberRepo = memberRepo;
        }

        // GET: api/games
        [HttpGet]
        public ActionResult<IEnumerable<GameReadDTO>> GetAllGames()
        {
            IEnumerable<Game> gameItems = _repo.GetAll();

            return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(gameItems));
        }

        // GET api/games/{id}
        [HttpGet("{id}", Name = "GetGameById")]
        public ActionResult<GameReadDTO> GetGameById(int id)
        {
            Game gameItem = _repo.GetById(id);

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
            Member memberItem = _memberRepo.GetById(id);
            IEnumerable<Game> gameItems = _repo.GetFutureGamesByMember(memberItem);

            return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(gameItems));
        }

        // POST: api/games
        [HttpPost]
        public ActionResult<GameReadDTO> CreateGame(GameCreateDTO gameCreateDTO)
        {
            Game gameModel = _mapper.Map<Game>(gameCreateDTO);

            _repo.Create(gameModel);
            _repo.SaveChanges();

            GameReadDTO gameReadDto = _mapper.Map<GameReadDTO>(gameModel);

            return CreatedAtRoute(nameof(GetGameById), new { gameReadDto.Id }, gameReadDto);
        }

        // PATCH: api/games/5
        [HttpPatch("{id}")]
        public ActionResult PartialGameUpdate(int id, JsonPatchDocument<GameUpdateDTO> patchDoc)
        {
            Game gameModelFromRepo = _repo.GetById(id);

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

            _repo.Update(gameModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // DELETE api/games/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            Game gameModelFromRepo = _repo.GetById(id);

            if (gameModelFromRepo == null)
            {
                return NotFound();
            }

            _repo.Delete(gameModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}

