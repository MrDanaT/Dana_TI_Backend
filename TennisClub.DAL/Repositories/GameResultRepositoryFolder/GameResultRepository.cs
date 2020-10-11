using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepositoryFolder
{
    public class GameResultRepository : Repository<GameResult>, IGameResultRepository
    {
        public GameResultRepository(TennisClubContext context)
          : base(context)
        { }

        public IEnumerable<GameResult> GetGameResultsByMember(Member member)
        {
            // TODO: Nakijken
            IQueryable<GameResult> gameResultItems = TennisClubContext.GameResults
                .AsNoTracking()
                .Where(gr => gr.GameNavigation.MemberId == member.Id)
                .Include(x => x.GameNavigation)
                .Select(gr => gr);

            return gameResultItems.AsEnumerable();
        }



        private TennisClubContext TennisClubContext => Context;
    }
}

