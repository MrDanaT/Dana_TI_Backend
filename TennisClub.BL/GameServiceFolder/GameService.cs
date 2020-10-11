using System.Collections.Generic;
using TennisClub.DAL.Entities;
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

        public IEnumerable<Game> GetAllGames()
        {
            IEnumerable<Game> gameItems = _unitOfWork.Games.GetAll();

            return gameItems;
        }

        public Game GetGameById(int id)
        {
            Game gameItem = _unitOfWork.Games.GetById(id);

            return gameItem;
        }

        public IEnumerable<Game> GetAllFutureGamesByMemberId(int id)
        {
            Member memberItem = _unitOfWork.Members.GetById(id);
            IEnumerable<Game> gameItems = _unitOfWork.Games.GetFutureGamesByMember(memberItem);

            return gameItems;
        }

        public void CreateGame(Game game)
        {
            _unitOfWork.Games.Create(game);
            _unitOfWork.Commit();
        }

        public void UpdateGame(Game game)
        {
            _unitOfWork.Commit();
        }

        public void DeleteGame(Game game)
        {
            _unitOfWork.Games.Delete(game);
            _unitOfWork.Commit();
        }
    }
}
