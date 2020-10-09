using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameRepository
{
    public interface IGameRepository : IRepository<Game>
    {
        IEnumerable<Game> GetFutureGamesByMember(Member member);
    }
}