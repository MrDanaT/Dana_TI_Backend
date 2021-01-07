using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common;
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
            if (member.IsNull())
            {
                throw new ArgumentNullException();
            }

            IQueryable<GameResult> gameResultItems = TennisClubContext.GameResults
                .AsNoTracking()
                .Where(gr => gr.GameNavigation.MemberId == member.Id)
                .Include(gr => gr.GameNavigation);

            return _mapper.Map<IEnumerable<GameResultReadDTO>>(gameResultItems);
        }

        public override void Delete(int id)
        {
            // Do nothing
        }

        public override IEnumerable<GameResultReadDTO> GetAll()
        {
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<GameResult, Member>? itemsFromDB = TennisClubContext.GameResults
                .Include(x => x.GameNavigation)
                .ThenInclude(x => x.LeagueNavigation)
                .Include(x => x.GameNavigation)
                .ThenInclude(x => x.MemberNavigation);

            if (!itemsFromDB.IsNull())
            {
                List<GameResult>? anderLijst = itemsFromDB.ToList();
            }

            return _mapper.Map<IEnumerable<GameResultReadDTO>>(itemsFromDB);
        }
    }
}