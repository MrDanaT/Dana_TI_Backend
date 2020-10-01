using System.Collections;
using System.Collections.Generic;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.LeagueRepository
{
    public interface ILeagueRepository
    {
        IEnumerable<League> GetAllLeagues();
    }
}