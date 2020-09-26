using System;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.GameRepository
{
    public interface IGameRepository : ISavable
    {
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
        void GetGamesByMemberAndDate(Member member, DateTime date);
    }
}