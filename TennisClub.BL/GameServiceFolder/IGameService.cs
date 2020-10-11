using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.GameServiceFolder
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();

        Game GetGameById(int id);

        IEnumerable<Game> GetAllFutureGamesByMemberId(int id);

        void CreateGame(Game game);

        void UpdateGame(Game game);

        void DeleteGame(Game game);
    }
}
