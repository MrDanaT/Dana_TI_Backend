using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.GameResult;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepositoryFolder
{
    public class GameResultRepository : Repository<GameResult, GameResultCreateDTO, GameResultReadDTO, GameResultUpdateDTO, int>, IGameResultRepository
    {
        public GameResultRepository(TennisClubContext context, IMapper mapper)
          : base(context, mapper)
        { }

        public IEnumerable<GameResultReadDTO> GetGameResultsByMember(MemberReadDTO member)
        {
            // TODO: Nakijken
            IQueryable<GameResult> gameResultItems = TennisClubContext.GameResults
                .AsNoTracking()
                .Where(gr => gr.GameNavigation.MemberId == member.Id)
                .Include(x => x.GameNavigation)
                .Select(gr => gr);

            return _mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems);
        }



        private TennisClubContext TennisClubContext => Context;
    }
}

