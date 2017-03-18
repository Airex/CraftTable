using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using CraftTable;
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

    public class HomeController : Controller
    {

        private class SessionHolder
        {
            public CraftTable.CraftTable CraftTable { get; set; } 
            public  SiteProgressWatcher ProgressWatcher { get; set; } 
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
            ViewBag.Title = "Home Page";

            var craftMan = new CraftMan(788, 851, 346, 60);
            var recipe = new Recipe(478, 60, 3140, 150);
            Crafter crafter = Crafter.BlackSmith;

            var homeViewModel = new HomeViewModel()
            {
                Abilities = _abilities.Select(ability => new AbilityViewModel() {Name = ability.Name(), XivDbId = ability.IdForCrafter(crafter)}).ToList(),
            };

            if (Session["CraftTable"] != null)
            {
                Session.Remove("CraftTable");
            }

            SiteProgressWatcher progressWatcher = new SiteProgressWatcher();
            var craftTable = _factory(recipe, craftMan, progressWatcher);
            SessionHolder holder = new SessionHolder() {CraftTable = craftTable, ProgressWatcher = progressWatcher};
            Session.Add("CraftTable", holder);

            return View(homeViewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Act(AbilityModel selectedAction)
        {
            var craftMan = new CraftMan(788, 851, 346, 60);
            var recipe = new Recipe(478, 60, 3140, 150);

            string message = null;

            var homeViewModel = new HomeViewModel();
          
            var holder = (SessionHolder)Session["CraftTable"];
            holder.ProgressWatcher.Clear();

            var table = holder.CraftTable;
            try
            {
                if (selectedAction.AbilityName != null)
                {
                    var firstOrDefault =
                        _abilities.FirstOrDefault(ability => ability.Name() == selectedAction.AbilityName);
                    table.Act(firstOrDefault);
                }
            }
            catch (CraftSuccessException ex)
            {
                message = $"Craft succeded. {(ex.IsHighQuality ? "HQ" : "NQ")} with chance {ex.Chance}";
            }
            catch (CraftFailedException)
            {

                message = "Craft Failed.";
            }
            catch (AbilityFailedException ex)
            {
                message = $"Ability Failed with chance {ex.Chance}.";
            }
            catch (AbilityNotAvailableException)
            {
                message = $"Ability is not available.";
            }
            catch (CraftAlreadyFinishedException)
            {
                message = $"Craft finished.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            var model = new StatsViewModel
            {
                Condition = table.Condition.ToString(),
                Durability = table.Durability,
                Progress = table.Progress,
                Quality = table.Quality,
                MaxDurability = recipe.Durability,
                MaxProgress = recipe.Difficulty,
                MaxQuality = recipe.MaxQuality,
                CraftPoints = table.CraftPoints,
                MaxCraftPoints = craftMan.MaxCraftPoints,
                HighQualityChance = table.HighQualityChance
            };

            homeViewModel.Stats = model;
            homeViewModel.Buffs = table.Buffs.Select(buff => new BuffViewModel()
            {
                Name = buff.GetType().Name,
                Stacks = (buff as IStacks)?.Stacks ?? 0,
                Steps = (buff as ISteps)?.Steps ?? 0,
                XivDbId = buff.Id()
            }).ToList();

            homeViewModel.Message = null;
            homeViewModel.Logs = holder.ProgressWatcher.Logs.Select(s => new LogViewModel() {Text = s}).ToList();


            return Json(homeViewModel);
        }
    }
}
