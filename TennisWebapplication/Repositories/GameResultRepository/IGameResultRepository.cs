using System;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.GameResultRepository
{
    public interface IGameResultRepository : ISavable
    {
        void CreateGameResult(GameResult gameResult);
        void UpdateGameResult(GameResult gameResult);
        void GetGameResultByMemberAndDate(Member member, DateTime date);
    }
}