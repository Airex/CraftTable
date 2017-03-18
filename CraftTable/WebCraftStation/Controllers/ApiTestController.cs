using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CraftTable;
using WebCraftStation.Models;

namespace WebCraftStation.Controllers
{
    public class ApiTestController : Controller
    {

        private readonly IEnumerable<Ability> _abilities;

        public ApiTestController(IEnumerable<Ability> abilities)
        {
            _abilities = abilities;
        }

        // GET: ApiTest
        public ActionResult Index()
        {
            return View(new ApiTestViewModel()
            {
                Abilities = _abilities.Select(ability => new AbilityViewModel() { Name = ability.Name(), XivDbId = ability.IdForCrafter(Crafter.Culinarian) }).ToList(),
            });
        }
    }
}