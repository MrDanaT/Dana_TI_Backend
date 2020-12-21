using System.Collections.Generic;
using TennisClub.Common.Gender;

namespace TennisClub.BL.GenderServiceFolder
{
    public interface IGenderService
    {
        IEnumerable<GenderReadDTO> GetAllGenders();
        GenderReadDTO GetGenderById(int id);
    }
}