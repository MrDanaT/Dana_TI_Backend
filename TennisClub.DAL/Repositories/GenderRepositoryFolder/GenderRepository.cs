using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.Gender;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.GenderRepositoryFolder
{
    public class GenderRepository : Repository<Gender, object, GenderReadDTO, object>, IGenderRepository
    {
        public GenderRepository(TennisClubContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override IEnumerable<GenderReadDTO> GetAll()
        {
            var gendersFromDb = Context.Genders.FromSqlRaw("SELECT * FROM tblGenders").AsNoTracking().ToList();
            return _mapper.Map<IEnumerable<GenderReadDTO>>(gendersFromDb);
        }
    }
}