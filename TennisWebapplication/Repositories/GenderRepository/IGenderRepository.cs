using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.GenderRepository
{
    public interface IGenderRepository
    {
        IEnumerable<Gender> GetAllGenders();
        Gender GetGenderById(byte id);
    }
}
