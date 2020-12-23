using System.Collections.Generic;
using TennisClub.Common.Game;
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

        public IEnumerable<GameReadDTO> GetGamesByMemberId(int id)
        {
            var memberItem = _unitOfWork.Members.GetById(id);
            var gameItems = _unitOfWork.Games.GetGamesByMember(memberItem);

            return gameItems;
        }

        public GameReadDTO CreateGame(GameCreateDTO game)
        {
            var createdGame = _unitOfWork.Games.Create(game);
            _unitOfWork.Commit();
            return createdGame;
        }

        public void DeleteGame(int id)
        {
            _unitOfWork.Games.Delete(id);
            _unitOfWork.Commit();
        }

        public void UpdateGame(int id, GameUpdateDTO updateDTO)
        {
            _unitOfWork.Games.Update(id, updateDTO);
            _unitOfWork.Commit();
        }
    }
}