using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BL.Entities;
using TennisClub.DAL.Repositories.GenderRepository;

namespace TennisWebapplication.Controllers
{
    public class GendersController : Controller
    {
        private readonly IGenderRepository _repo;

        public GendersController(IGenderRepository repo)
        {
            _repo = repo;
        }

        public ActionResult<IEnumerable<Gender>> GetAllGenders()
        {
            IEnumerable<Gender> genderItems = _repo.GetAllGenders();

            return Ok(genderItems);
        }
    }
}
