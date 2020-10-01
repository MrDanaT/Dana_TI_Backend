using System;
using System.Collections;
using System.Collections.Generic;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.GameRepository
{
    public interface IGameRepository : ISavable
    {
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(Game game);
        IEnumerable<Game> GetGamesByMember(Member member);
        IEnumerable<Game> GetFutureGamesByMember(Member member);
    }
}