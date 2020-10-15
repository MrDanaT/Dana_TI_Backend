using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.Game;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameRepositoryFolder
{
    public class GameRepository : Repository<Game, GameCreateDTO, GameReadDTO, GameUpdateDTO>, IGameRepository
    {
        public GameRepository(TennisClubContext context, IMapper mapper)
          : base(context, mapper)
        { }

        public IEnumerable<GameReadDTO> GetFutureGamesByMember(MemberReadDTO memberParam)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Game> gameItems = TennisClubContext.Games
                .AsNoTracking()
                .Where(g => g.Date > DateTime.Today && g.MemberId == memberParam.Id)
                .Select(g => g);

            return _mapper.Map<IEnumerable<GameReadDTO>>(gameItems.ToList());
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
