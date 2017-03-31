using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CraftTable;
using CraftTable.Abilities;
using CraftTable.Abilities.Specialist;
using CraftTable.Buffs;
using CraftTable.Contracts;
using CraftTable.Exceptions;
using WebCraftStation.Models;

namespace WebCraftStation.Controllers
{
    public class SiteProgressWatcher : IProgressWatcher
    {
        public IList<string> Logs { get; set; } = new List<string>();

        public void Log(string s)
        {
            Logs.Add(s);
        }

        public void Clear()
        {
            Logs.Clear();
        }
    }

    internal class AbilityCheckCraftService : ICraftActions
    {
        public int CraftPoints { get; set; }
        public void RestoreCraftPoints(int craftPoints)
        {

        }

        public void RestoreDurability(int durability)
        {

        }

        public void ApplyBuff(IBuff buff)
        {

        }

        public void Synth(SynthDelegate synth)
        {

        }

        public void Touch(int efficiency)
        {

        }

        public void UseCraftPoints(int craftPoints)
        {
            CraftPoints = craftPoints;
        }

        public void UseDurability(int durability)
        {

        }

        public T CalculateDependency<T>(CalculateDependency<T> input) where T : struct
        {
            return default(T);
        }

        public int CheckAbilityCost(Ability ability)
        {
            CraftPoints = 0;
            ability.Execute(this, true);
            return CraftPoints;
        }

        public void QueueAbility(Ability finishingTouches)
        {
            
        }
    }

    public class HomeController : Controller
    {

        private class SessionHolder
        {
            public CraftTable.CraftTable CraftTable { get; set; }
            public SiteProgressWatcher ProgressWatcher { get; set; }
            public CraftMan CraftMan { get; set; }
            public Recipe Recipe { get; set; }
        }

        private readonly IEnumerable<Ability> _abilities;
        private readonly CraftTable.CraftTable.Factory _factory;

        public HomeController(IEnumerable<Ability> abilities, CraftTable.CraftTable.Factory factory)
        {


            _abilities = abilities;
            _factory = factory;
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Web Craft Station";
            return View();
        }
   
    }
}
