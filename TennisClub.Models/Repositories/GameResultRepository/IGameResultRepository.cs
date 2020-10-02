using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepository
{
    public interface IGameResultRepository : ISavable
    {
        void CreateGameResult(GameResult gameResult);
        void UpdateGameResult(GameResult gameResult);
        IEnumerable<GameResult> GetGameResultsByMember(Member member);
    }
}