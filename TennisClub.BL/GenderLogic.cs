using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.GenderRepository;

namespace TennisClub.BL
{
   public class GenderLogic
    {
        private readonly IGenderRepository _repo;

        public GenderLogic(IGenderRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Gender> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _repo.GetAll();

            return genderItems;
        }

        public Gender GetGenderById(int id)
        {
            Gender genderFromRepo = _repo.GetById(id);

            return genderFromRepo;
        }
    }
}
