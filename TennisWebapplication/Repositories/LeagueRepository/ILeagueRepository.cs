using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.LeagueRepository
{
    public interface ILeagueRepository : ISavable
    {
        void GetAllLeagues();
        void GetLeagueById(byte id);
    }
}