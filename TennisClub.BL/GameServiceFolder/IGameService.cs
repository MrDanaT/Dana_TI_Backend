using System.Collections.Generic;
using TennisClub.Common.Game;

namespace TennisClub.BL.GameServiceFolder
{
    public interface IGameService
    {
        IEnumerable<GameReadDTO> GetAllGames();

        GameReadDTO GetGameById(int id);

        IEnumerable<GameReadDTO> GetAllFutureGamesByMemberId(int id);

        GameReadDTO CreateGame(GameCreateDTO game);

        void UpdateGame(int id, GameUpdateDTO updateDTO);

        void DeleteGame(int id);
    }
}