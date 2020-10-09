using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepository
{
    public interface IGenderRepository
    {
        IEnumerable<Gender> GetAllGenders();
        Gender GetGenderById(int id);
    }
}
