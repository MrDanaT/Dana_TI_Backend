using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.GameResult;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepositoryFolder
{
    public class GameResultRepository : Repository<GameResult, GameResultCreateDTO, GameResultReadDTO, GameResultUpdateDTO>, IGameResultRepository
    {
        public GameResultRepository(TennisClubContext context, IMapper mapper)
          : base(context, mapper)
        { }

        public override IEnumerable<GameResultReadDTO> GetAll()
        {
            IQueryable<GameResult> gameResultItems = TennisClubContext.GameResults
                .AsNoTracking()
                .Include(g => g.GameNavigation);

            return _mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems);
        }

        public IEnumerable<GameResultReadDTO> GetGameResultsByMember(MemberReadDTO member)
        {
            // TODO: Nakijken
            IQueryable<GameResult> gameResultItems = TennisClubContext.GameResults
                .AsNoTracking()
                .Where(gr => gr.GameNavigation.MemberId == member.Id)
                .Include(g => g.GameNavigation);

            return _mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems);
        }

        public override void Delete(int id)
        {
            // Do nothing
        }

        private TennisClubContext TennisClubContext => Context;
    }
}

