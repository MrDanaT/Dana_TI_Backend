using System;
using System.Collections.Generic;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.GameResultRepository
{
    public interface IGameResultRepository : ISavable
    {
        void CreateGameResult(GameResult gameResult);
        void UpdateGameResult(GameResult gameResult);
        IEnumerable<GameResult> GetGameResultsByMember(Member member);
    }
}