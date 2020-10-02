using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GameRepository
{
    public interface IGameRepository : ISavable
    {
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
        IEnumerable<Game> GetGamesByMember(Member member);
        IEnumerable<Game> GetFutureGamesByMember(Member member);
    }
}