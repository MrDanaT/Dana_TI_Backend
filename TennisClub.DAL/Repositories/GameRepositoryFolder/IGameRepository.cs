using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameRepositoryFolder
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetFutureGamesByMember(Member member);
    }
}