﻿using System.Collections.Generic;
using TennisClub.Common.GameResult;
using TennisClub.Common.Member;

namespace TennisClub.DAL.Repositories.GameResultRepositoryFolder
{
    public interface IGameResultRepository : IRepository<GameResultCreateDTO, GameResultReadDTO, GameResultUpdateDTO>
    {
        IEnumerable<GameResultReadDTO> GetGameResultsByMember(MemberReadDTO member);
    }
}