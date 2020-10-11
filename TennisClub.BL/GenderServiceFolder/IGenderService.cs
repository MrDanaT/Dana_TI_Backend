using System.Collections.Generic;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.GenderServiceFolder
{
    public interface IGenderService
    {
        IEnumerable<Gender> GetAllGenders();
        Gender GetGenderById(byte id);
    }
}
