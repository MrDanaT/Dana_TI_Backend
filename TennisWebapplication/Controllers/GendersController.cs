using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.GameRepository;
using TennisWebapplication.Repositories.GenderRepository;

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
