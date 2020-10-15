using System.Collections.Generic;
using TennisClub.Common.GameResult;
using TennisClub.Common.Member;
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

        public IEnumerable<GameResultReadDTO> GetAllGameResults()
        {
            IEnumerable<GameResultReadDTO> gameResultItems = _unitOfWork.GameResults.GetAll();

            return gameResultItems;
        }

        public GameResultReadDTO GetGameResultById(int id)
        {
            GameResultReadDTO gameResultItem = _unitOfWork.GameResults.GetById(id);

            return gameResultItem;
        }

        public GameResultReadDTO CreateGameResult(GameResultCreateDTO gameResult)
        {
            return _unitOfWork.GameResults.Create(gameResult);
        }

        public IEnumerable<GameResultReadDTO> GetGameResultsByMember(int id)
        {
            MemberReadDTO memberItem = _unitOfWork.Members.GetById(id);
            IEnumerable<GameResultReadDTO> gameResultItems = _unitOfWork.GameResults.GetGameResultsByMember(memberItem);

            return gameResultItems;
        }

        public GameResultUpdateDTO GetUpdateDTOByReadDTO(GameResultReadDTO entity)
        {
            return _unitOfWork.GameResults.GetUpdateDTOByReadDTO(entity);
        }

        public void UpdateGameResult(GameResultUpdateDTO gameResultToPatch, GameResultReadDTO gameResultModelFromRepo)
        {
            _unitOfWork.GameResults.MapUpdateDTOToReadDTO(gameResultToPatch, gameResultModelFromRepo);
            _unitOfWork.Commit();
        }
    }
}
