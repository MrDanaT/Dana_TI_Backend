using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameRepositoryFolder
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(TennisClubContext context)
          : base(context)
        { }

        public IEnumerable<Game> GetFutureGamesByMember(Member member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Game> gameItems = TennisClubContext.Games
                .AsNoTracking()
                .Where(g => g.Date > DateTime.Today)
                .Select(g => g);

            return gameItems.ToList();
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
