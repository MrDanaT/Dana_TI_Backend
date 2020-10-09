using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GameRepository
{
    public class GameRepository : IGameRepository
    {
        private readonly TennisClubContext _context;

        public GameRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            _context.Games.Add(game);
        }

        public void Delete(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            _context.Games.Remove(game);
        }

        public IEnumerable<Game> GetFutureGamesByMember(Member member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Game> gameItems = _context.Games
                .AsNoTracking()
                .Where(g => g.Date > DateTime.Today)
                .Select(g => g);

            return gameItems.AsEnumerable();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(Game game)
        {
            //Nothing
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.AsNoTracking().ToList();
        }

        public Game GetGameById(int id)
        {
            return _context.Games.FirstOrDefault(g => g.Id == id);
        }
    }
}
