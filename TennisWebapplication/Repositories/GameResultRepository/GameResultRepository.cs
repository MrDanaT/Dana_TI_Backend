using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.GameResultRepository
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
                throw new ArgumentNullException(nameof(gameResult));

            _context.GameResults.Add(gameResult);
        }

        public IEnumerable<GameResult> GetGameResultsByMember(Member member)
        {
            throw new NotImplementedException();
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
