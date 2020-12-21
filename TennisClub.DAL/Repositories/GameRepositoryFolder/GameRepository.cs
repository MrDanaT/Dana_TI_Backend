using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TennisClub.Common.Game;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameRepositoryFolder
{
    public class GameRepository : Repository<Game, GameCreateDTO, GameReadDTO, GameUpdateDTO>, IGameRepository
    {
        public GameRepository(TennisClubContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private TennisClubContext TennisClubContext => Context;

        public IEnumerable<GameReadDTO> GetGamesByMember(MemberReadDTO memberParam)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            var gameItems = TennisClubContext.Games
                .AsNoTracking()
                .Where(g => g.MemberId == memberParam.Id)
                .Select(g => g)
                .ToList();

            gameItems.Sort();

            return _mapper.Map<IEnumerable<GameReadDTO>>(gameItems.ToList());
        }

        public override GameReadDTO Create(GameCreateDTO entity)
        {
            var memberFromRepo = Context.Members.Find(entity.MemberId);
            var isMember = false;

            foreach (var memberRole in memberFromRepo.MemberRoles)
            {
                var hasSpelerRole = memberRole.RoleNavigation.Name.Equals("Speler");
                var isStillASpeler = memberRole.EndDate != null;
                if (hasSpelerRole && isStillASpeler)
                {
                    isMember = true;
                    break;
                }
            }

            if (isMember)
                return base.Create(entity);
            return null;
        }
    }
}