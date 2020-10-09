using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepository
{
    public interface IGameResultRepository : IUpdatable<GameResult>
    {
        IEnumerable<GameResult> GetGameResultsByMember(Member member);
        IEnumerable<GameResult> GetAllGameResults();
        GameResult GetGameResultById(int id);
    }
}