using System;
using System.Linq;
using System.Web.Mvc;
using CraftTable.Abilities;
using CraftTable.Abilities.Specialist;
using CraftTable.Buffs;
namespace WebCraftStation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Web Craft Station";
            return View();
        }
   
    }
}
