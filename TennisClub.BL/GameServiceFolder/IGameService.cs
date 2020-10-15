using System.Collections.Generic;
using TennisClub.Common.Game;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.GameServiceFolder
{
    public interface IGameService
    {
        IEnumerable<GameReadDTO> GetAllGames();

        GameReadDTO GetGameById(int id);

        IEnumerable<GameReadDTO> GetAllFutureGamesByMemberId(int id);

        GameReadDTO CreateGame(GameCreateDTO game);

        void UpdateGame(GameUpdateDTO gameToPatch, GameReadDTO gameModelFromRepo);

        void DeleteGame(GameReadDTO game);
        GameUpdateDTO GetUpdateDTOByReadDTO(GameReadDTO entity);
    }
}
