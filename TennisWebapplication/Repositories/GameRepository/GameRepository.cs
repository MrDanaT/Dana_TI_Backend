using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.GameRepository
{
    public class GameRepository : IGameRepository
    {
        private readonly TennisClubContext _context;

        public GameRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void CreateGame(Game game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            _context.Games.Add(game);
        }

        public void DeleteGame(Game game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            _context.Games.Remove(game);
        }

        public IEnumerable<Game> GetFutureGamesByMember(Member member)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetGamesByMember(Member member)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateGame(Game game)
        {
            //Nothing
        }
    }
}
