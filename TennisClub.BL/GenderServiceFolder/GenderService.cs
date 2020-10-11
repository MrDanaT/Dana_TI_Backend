using System.Collections.Generic;
using TennisClub.DAL.Entities;
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

        public IEnumerable<Gender> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _unitOfWork.Genders.GetAll();

            return genderItems;
        }

        public Gender GetGenderById(byte id)
        {
            Gender genderFromRepo = _unitOfWork.Genders.GetById(id);

            return genderFromRepo;
        }

    }
}
