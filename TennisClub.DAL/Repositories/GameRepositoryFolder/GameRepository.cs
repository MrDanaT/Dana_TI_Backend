using System;
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

        public override IEnumerable<GameReadDTO> GetAll()
        {
            var itemsFromDB = TennisClubContext.Games
                .AsNoTracking()
                .Include(g => g.LeagueNavigation)
                .Include(g => g.MemberNavigation);

            return _mapper.Map<IEnumerable<GameReadDTO>>(itemsFromDB);
        }

        public override GameReadDTO GetById(int id)
        {
            if (id < 0) throw new NullReferenceException("Id is out of range");

            var itemFromDB = TennisClubContext.Games.Find(id);

            if (itemFromDB == null) throw new NullReferenceException("Object not found");

            itemFromDB.MemberNavigation = TennisClubContext.Members.Find(itemFromDB.MemberId);
            itemFromDB.LeagueNavigation = TennisClubContext.Leagues.Find(itemFromDB.LeagueId);

            return _mapper.Map<GameReadDTO>(itemFromDB);
        }

        public IEnumerable<GameReadDTO> GetGamesByMember(MemberReadDTO memberParam)
        {
            var gameItems = TennisClubContext.Games
                .AsNoTracking()
                .Where(g => g.MemberId == memberParam.Id)
                .Select(g => g)
                .Include(g => g.LeagueNavigation)
                .Include(g => g.MemberNavigation)
                .ToList();

            gameItems.Sort((game, game1) => game.Date.CompareTo(game1.Date));

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

            return isMember ? base.Create(entity) : null;
        }
    }
}