using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepositoryFolder
{
    public interface IGenderRepository : IRepository< object, GenderReadDTO, object, byte>
    {
    }
}
