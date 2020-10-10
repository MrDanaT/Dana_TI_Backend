using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.GameRepository;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.BL
{
    public class GameLogic
    {
        private readonly IGameRepository _repo;
        private readonly IMemberRepository _memberRepo;

        public GameLogic(IGameRepository repo, IMemberRepository memberRepo)
        {
            _repo = repo;
            _memberRepo = memberRepo;
        }

        public IEnumerable<Game> GetAllGames()
        {
            IEnumerable<Game> gameItems = _repo.GetAll();

            return gameItems;
        }

        public Game GetGameById(int id)
        {
            Game gameItem = _repo.GetById(id);

            return gameItem;
        }

        public IEnumerable<Game> GetAllFutureGamesByMemberId(int id)
        {
            Member memberItem = _memberRepo.GetById(id);
            IEnumerable<Game> gameItems = _repo.GetFutureGamesByMember(memberItem);

            return gameItems;
        }

        public void CreateGame(Game game) {

            _repo.Create(game);
            _repo.SaveChanges();
        }

        public void PartialGameUpdate(Game game)
        {
            _repo.Update(game);
            _repo.SaveChanges();
        }

        public void DeleteGame(Game game)
        {
            _repo.Delete(game);
            _repo.SaveChanges();
        }
    }
}
