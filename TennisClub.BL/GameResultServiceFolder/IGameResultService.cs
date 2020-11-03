using System.Collections.Generic;
using TennisClub.Common.GameResult;

namespace TennisClub.BL.GameResultServiceFolder
{
    public interface IGameResultService
    {
        IEnumerable<GameResultReadDTO> GetAllGameResults();
        GameResultReadDTO GetGameResultById(int id);
        GameResultReadDTO CreateGameResult(GameResultCreateDTO gameResult);
        void UpdateGameResult(int id, GameResultUpdateDTO updateDTO);
        IEnumerable<GameResultReadDTO> GetGameResultsByMember(int id);
    }
}
