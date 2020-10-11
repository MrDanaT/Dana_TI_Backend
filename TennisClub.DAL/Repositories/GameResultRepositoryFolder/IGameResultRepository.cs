using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepositoryFolder
{
    public interface IGameResultRepository : IRepository<GameResult>
    {
        IEnumerable<GameResult> GetGameResultsByMember(Member member);
    }
}