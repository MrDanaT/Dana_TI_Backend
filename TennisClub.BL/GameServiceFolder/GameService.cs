using System.Collections.Generic;
using TennisClub.Common.Game;
using TennisClub.Common.Member;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.GameServiceFolder
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GameReadDTO> GetAllGames()
        {
            return _unitOfWork.Games.GetAll();
        }

        public GameReadDTO GetGameById(int id)
        {
            return _unitOfWork.Games.GetById(id);
        }

        public IEnumerable<GameReadDTO> GetAllFutureGamesByMemberId(int id)
        {
            MemberReadDTO memberItem = _unitOfWork.Members.GetById(id);
            IEnumerable<GameReadDTO> gameItems = _unitOfWork.Games.GetFutureGamesByMember(memberItem);

            return gameItems;
        }

        public GameReadDTO CreateGame(GameCreateDTO game)
        {
            GameReadDTO createdGame = _unitOfWork.Games.Create(game);
            _unitOfWork.Commit();
            return createdGame;
        }

        public void DeleteGame(GameReadDTO game)
        {
            _unitOfWork.Games.Delete(game);
            _unitOfWork.Commit();
        }

        public GameUpdateDTO GetUpdateDTOByReadDTO(GameReadDTO entity)
        {
            return _unitOfWork.Games.GetUpdateDTOByReadDTO(entity);
        }

        public void UpdateGame(GameUpdateDTO gameToPatch, GameReadDTO gameModelFromRepo)
        {
            _unitOfWork.Games.MapUpdateDTOToReadDTO(gameToPatch, gameModelFromRepo);
            _unitOfWork.Commit();
        }
    }
}
