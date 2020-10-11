using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.GameResultServiceFolder
{
    public interface IGameResultService
    {
        IEnumerable<GameResult> GetAllGameResults();
        GameResult GetGameResultById(int id);
        void CreateGameResult(GameResult gameResult);
        void UpdateGameResult(GameResult gameResult);
        IEnumerable<GameResult> GetGameResultsByMember(int id);
    }
}
