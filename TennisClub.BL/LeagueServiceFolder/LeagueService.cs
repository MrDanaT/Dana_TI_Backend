using System.Collections.Generic;
using TennisClub.Common.League;
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

        public IEnumerable<LeagueReadDTO> GetAllLeagues()
        {
            var leagueItems = _unitOfWork.Leagues.GetAll();
            return leagueItems;
        }

        public LeagueReadDTO GetLeagueById(int id)
        {
            var leagueFromRepo = _unitOfWork.Leagues.GetById(id);

            return leagueFromRepo;
        }
    }
}