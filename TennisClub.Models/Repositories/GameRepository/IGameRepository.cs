using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GameRepository
{
    public interface IGameRepository : IUpdatable<Game>, IDeletable<Game>
    {
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> GetFutureGamesByMember(Member member);
        Game GetGameById(int id);
    }
}