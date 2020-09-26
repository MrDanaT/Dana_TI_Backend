using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.GenderRepository
{
    interface IGenderRepository
    {
        IEnumerable<Gender> GetAllGenders();
        Gender GetGenderById(int id);
    }
}
