using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<GameReadDTO> GetGamesByMember(MemberReadDTO memberParam)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            List<Game> gameItems = TennisClubContext.Games
                .AsNoTracking()
                .Where(g => g.MemberId == memberParam.Id)
                .Select(g => g)
                .ToList();

            gameItems.Sort();

            return _mapper.Map<IEnumerable<GameReadDTO>>(gameItems.ToList());
        }

        public override GameReadDTO Create(GameCreateDTO entity)
        {
            Member memberFromRepo = Context.Members.Find(entity.MemberId);
            bool isMember = false;

            foreach (MemberRole memberRole in memberFromRepo.MemberRoles)
            {
                bool hasSpelerRole = memberRole.RoleNavigation.Name.Equals("Speler");
                bool isStillASpeler = memberRole.EndDate != null;
                if (hasSpelerRole && isStillASpeler)
                {
                    isMember = true;
                    break;
                }
            }

            if (isMember)
            {
                return base.Create(entity);
            }
            else
            {
                return null;
            }
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
