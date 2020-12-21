using System.Collections.Generic;
using TennisClub.Common.Gender;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.GenderServiceFolder
{
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GenderReadDTO> GetAllGenders()
        {
            return _unitOfWork.Genders.GetAll();
        }

        public GenderReadDTO GetGenderById(int id)
        {
            return _unitOfWork.Genders.GetById(id);
        }
    }
}