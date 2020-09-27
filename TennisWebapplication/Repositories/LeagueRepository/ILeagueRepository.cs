using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.LeagueRepository
{
    public interface ILeagueRepository
    {
        void GetAllLeagues();
        void GetLeagueById(byte id);
    }
}