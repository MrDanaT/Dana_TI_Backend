using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.LeagueRepository;

namespace TennisClub.BL
{
    public class LeagueLogic
    {
        private readonly ILeagueRepository _repo;

        public LeagueLogic(ILeagueRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<League> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _repo.GetAll();

            return leagueItems;
        }

        public League GetLeagueById(int id)
        {
            League leagueFromRepo = _repo.GetById(id);

            return leagueFromRepo;
        }
    }
}
