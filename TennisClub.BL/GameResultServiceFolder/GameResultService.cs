using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.GameResultServiceFolder
{
    public class GameResultService : IGameResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GameResult> GetAllGameResults()
        {
            IEnumerable<GameResult> gameResultItems = _unitOfWork.GameResults.GetAll();

            return gameResultItems;
        }

        public GameResult GetGameResultById(int id)
        {
            GameResult gameResultItem = _unitOfWork.GameResults.GetById(id);

            return gameResultItem;
        }

        public void CreateGameResult(GameResult gameResult)
        {
            _unitOfWork.GameResults.Create(gameResult);
        }

        public IEnumerable<GameResult> GetGameResultsByMember(int id)
        {
            Member memberItem = _unitOfWork.Members.GetById(id);
            IEnumerable<GameResult> gameResultItems = _unitOfWork.GameResults.GetGameResultsByMember(memberItem);

            return gameResultItems;
        }

        public void UpdateGameResult(GameResult gameResult)
        {
            _unitOfWork.Commit();
        }
    }
}
