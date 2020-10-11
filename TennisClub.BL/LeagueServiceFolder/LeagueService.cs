using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.LeagueServiceFolder
{
    public class LeagueService : ILeagueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeagueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<League> GetAllLeagues()
        {
            IEnumerable<League> leagueItems = _unitOfWork.Leagues.GetAll();

            return leagueItems;
        }

        public League GetLeagueById(byte id)
        {
            League leagueFromRepo = _unitOfWork.Leagues.GetById(id);

            return leagueFromRepo;
        }
    }
}
