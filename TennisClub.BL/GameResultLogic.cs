using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.GameResultRepository;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.BL
{
    public class GameResultLogic
    {
        private readonly IGameResultRepository _repo;
        private readonly IMemberRepository _memberRepo;

        public GameResultLogic(IGameResultRepository repo, IMemberRepository memberRepo)
        {
            _repo = repo;
            _memberRepo = memberRepo;
        }

        public IEnumerable<GameResult> GetAllGameResults()
        {
            IEnumerable<GameResult> gameResultItems = _repo.GetAll();

            return gameResultItems;
        }

        public GameResult GetGameResultById(int id)
        {
            GameResult gameResultItem = _repo.GetById(id);

            return gameResultItem;
        }

        public void CreateGameResult(GameResult gameResult)
        {
            _repo.Create(gameResult);
            _repo.SaveChanges();
        }

        public void PartialGameResultUpdate(GameResult gameResult)
        {
            _repo.Update(gameResult);
            _repo.SaveChanges();
        }

        public IEnumerable<GameResult> GetGameResultsByMember(int id)
        {
            Member memberItem = _memberRepo.GetById(id);
            IEnumerable<GameResult> gameResultItems = _repo.GetGameResultsByMember(memberItem);

            return gameResultItems;
        }
    }
}
