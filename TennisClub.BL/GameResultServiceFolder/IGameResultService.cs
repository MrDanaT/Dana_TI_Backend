using System.Collections.Generic;
using TennisClub.Common.GameResult;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.GameResultServiceFolder
{
    public interface IGameResultService
    {
        IEnumerable<GameResultReadDTO> GetAllGameResults();
        GameResultReadDTO GetGameResultById(int id);
        GameResultReadDTO CreateGameResult(GameResultCreateDTO gameResult);
        void UpdateGameResult(GameResultUpdateDTO gameResultToPatch, GameResultReadDTO gameResultModelFromRepo);
        IEnumerable<GameResultReadDTO> GetGameResultsByMember(int id);
        GameResultUpdateDTO GetUpdateDTOByReadDTO(GameResultReadDTO entity);
    }
}
