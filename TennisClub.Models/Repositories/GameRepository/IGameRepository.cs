using System;
using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GameRepository
{
    public interface IGameRepository : IUpdatable
    {
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> GetFutureGamesByMember(Member member);
        Game GetGameById(int id);
    }
}