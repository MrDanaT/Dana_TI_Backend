using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TennisClub.Common.GameResult;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepositoryFolder
{
    public class GameResultRepository :
        Repository<GameResult, GameResultCreateDTO, GameResultReadDTO, GameResultUpdateDTO>, IGameResultRepository
    {
        public GameResultRepository(TennisClubContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private TennisClubContext TennisClubContext => Context;

        public IEnumerable<GameResultReadDTO> GetGameResultsByMember(MemberReadDTO member)
        {
            if (member == null) throw new ArgumentNullException();
            
            IQueryable<GameResult> gameResultItems = TennisClubContext.GameResults
                .AsNoTracking()
                .Where(gr => gr.GameNavigation.MemberId == member.Id);

            return _mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems);
        }

        public override void Delete(int id)
        {
            // Do nothing
        }
    }
}