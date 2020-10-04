using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GameResultRepository
{
    public class GameResultRepository : IGameResultRepository
    {
        private readonly TennisClubContext _context;


        public GameResultRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void CreateGameResult(GameResult gameResult)
        {
            if (gameResult == null)
            {
                throw new ArgumentNullException(nameof(gameResult));
            }

            _context.GameResults.Add(gameResult);
        }

        public IEnumerable<GameResult> GetAllGameResults(Member member)
        {
            return _context.GameResults.AsNoTracking().ToList();
        }

        public IEnumerable<GameResult> GetGameResultsByMember(Member member)
        {
            // TODO: Nakijken
            var gameResultItems = _context.GameResults
                .AsNoTracking()
                .Where(gr => gr.GameNavigation.MemberId == member.Id)
                .Include(x => x.GameNavigation)
                .Select(gr => gr);

            return gameResultItems.AsEnumerable();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateGameResult(GameResult gameResult)
        {
            //Nothing
        }
    }
}
