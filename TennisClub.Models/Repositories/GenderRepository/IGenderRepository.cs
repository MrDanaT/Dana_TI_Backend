using System.Collections.Generic;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepository
{
    public interface IGenderRepository
    {
        IEnumerable<Gender> GetAllGenders();
    }
}
