using System.Collections.Generic;
using TennisClub.Common.Game;
using TennisClub.Common.Member;

namespace TennisClub.DAL.Repositories.GameRepositoryFolder
{
    public interface IGameRepository : IRepository<GameCreateDTO, GameReadDTO, GameUpdateDTO>
    {
        IEnumerable<GameReadDTO> GetGamesByMember(MemberReadDTO member);
    }
}