using System.Collections.Generic;
using TennisClub.Common.Game;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GameRepositoryFolder
{
    public interface IGameRepository : IRepository< GameCreateDTO, GameReadDTO, GameUpdateDTO>
    {
        IEnumerable<GameReadDTO> GetFutureGamesByMember(MemberReadDTO member);
    }
}