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

    internal class AbilityCheckCraftService:ICraftActions
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
            ability.Execute(this,true);
            return CraftPoints;
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

            var craftMan = new CraftMan(995, 995, 437, 60);
            var recipe = new Recipe(1968, 70, 13187, 190);
            Crafter crafter = Crafter.BlackSmith;

            

            if (Session["CraftTable"] != null)
            {
                Session.Remove("CraftTable");
            }

            SiteProgressWatcher progressWatcher = new SiteProgressWatcher();
            var craftTable = _factory(recipe, craftMan, progressWatcher);

             AbilityCheckCraftService abilityCheckCraftService = new AbilityCheckCraftService();

            var homeViewModel = new HomeViewModel()
            {
                Abilities = _abilities.Select(ability => new AbilityViewModel()
                {
                    Name = ability.Name(),
                    XivDbId = ability.IdForCrafter(crafter),
                    IsEnabled = craftTable.CanAct(ability),
                    CraftPointsCost = abilityCheckCraftService.CheckAbilityCost(ability)
                }).ToList(),
            };
            SessionHolder holder = new SessionHolder() {CraftTable = craftTable, ProgressWatcher = progressWatcher};
            Session.Add("CraftTable", holder);

            return View(homeViewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Act(AbilityModel selectedAction)
        {
            var craftMan = new CraftMan(995, 995, 437, 60);
            var recipe = new Recipe(1968, 70, 13187, 190);

            string message = null;

            var homeViewModel = new HomeViewModel();
          
            var holder = (SessionHolder)Session["CraftTable"];
            holder.ProgressWatcher.Clear();

            var table = holder.CraftTable;
            try
            {
                if (selectedAction.AbilityName != null)
                {
                    var firstOrDefault = _abilities.FirstOrDefault(ability => ability.Name() == selectedAction.AbilityName);
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
            homeViewModel.Abilities =
                _abilities.Select(ability => new AbilityViewModel() {Name = ability.Name(), IsEnabled = table.CanAct(ability)}).ToList();
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
