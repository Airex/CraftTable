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
    }

    public class HomeController : Controller
    {

        private class SessionHolder
        {
            public CraftTable.CraftTable CraftTable { get; set; }
            public SiteProgressWatcher ProgressWatcher { get; set; }
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
            var crafter = Crafter.GoldSmith;



            if (Session["CraftTable"] != null)
            {
                Session.Remove("CraftTable");
            }

            var progressWatcher = new SiteProgressWatcher();
            var craftTable = _factory(recipe, craftMan, progressWatcher);

            var abilityCheckCraftService = new AbilityCheckCraftService();

            var homeViewModel = new HomeViewModel
            {
                Abilities = _abilities.Select(ability =>
                {
                    var abilityDescriptorAttribute = ability.AbilityDescriptor();
                    return new AbilityViewModel()
                    {
                        Name = ability.Name(),
                        XivDbId = ability.IdForCrafter(crafter),
                        IsEnabled = craftTable.CanAct(ability),
                        CraftPointsCost = abilityCheckCraftService.CheckAbilityCost(ability),
                        Category = abilityDescriptorAttribute.Category.ToString(),
                        Order = abilityDescriptorAttribute.Order
                    };
                }).ToList(),
            };
            var holder = new SessionHolder { CraftTable = craftTable, ProgressWatcher = progressWatcher };
            Session.Add("CraftTable", holder);

            return View(homeViewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Act(AbilityModel selectedAction)
        {
            var craftMan = new CraftMan(995, 995, 437, 60);
            var recipe = new Recipe(1968, 70, 13187, 190);

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
            catch (CraftSuccessException)
            {

            }
            catch (CraftFailedException)
            {

            }
            catch (AbilityFailedException)
            {

            }
            catch (AbilityNotAvailableException)
            {

            }
            catch (CraftAlreadyFinishedException)
            {

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            var craftTableInfo = table.GetStatus();

            var model = new StatsViewModel
            {
                Step = craftTableInfo.Step,
                Condition = craftTableInfo.Condition.ToString(),
                Durability = craftTableInfo.Durability,
                Progress = craftTableInfo.Progress,
                Quality = craftTableInfo.Quality,
                MaxDurability = recipe.Durability,
                MaxProgress = recipe.Difficulty,
                MaxQuality = recipe.MaxQuality,
                CraftPoints = craftTableInfo.CraftPoints,
                MaxCraftPoints = craftMan.MaxCraftPoints,
                HighQualityChance = craftTableInfo.HighQualityChance
            };

            homeViewModel.Stats = model;
            homeViewModel.Abilities = _abilities.Select(ability => new AbilityViewModel
            {
                Name = ability.Name(), IsEnabled = table.CanAct(ability), IsHighLigthed = CheckHighLight(ability, craftTableInfo)
            }).ToList();
            homeViewModel.Buffs = craftTableInfo.Buffs.Select(buff => new BuffViewModel
            {
                Name = buff.Type.Name,
                Stacks = buff.Stacks,
                Steps = buff.Steps,
                XivDbId = buff.XivDb
            }).ToList();

            homeViewModel.Message = null;
            homeViewModel.Logs = holder.ProgressWatcher.Logs.Select(s => new LogViewModel { Text = s }).ToList();


            return Json(homeViewModel);
        }

        private bool CheckHighLight(Ability ability, CraftTableInfo craftTableInfo)
        {
            var type = ability.GetType();
            if (type == typeof(TricksOfTheTrade) || type == typeof(PreciseTouch))
            {
                return craftTableInfo.Condition.IsGoodOrExcellent();
            }

            if (type == typeof(ByregotsBlessing) || type == typeof(ByregotsMiracle))
            {
                return craftTableInfo.Buffs.Any(info => info.Type == typeof(InnerQuietBuff) && info.Stacks >= 2);
            }

            if (type == typeof(ByregotsBrow))
            {
                return craftTableInfo.Buffs.Any(info => info.Type == typeof(InnerQuietBuff) && info.Stacks >= 2) && craftTableInfo.Condition.IsGoodOrExcellent();
            }

            if (type == typeof(Satisfaction))
            {
                return craftTableInfo.Buffs.Any(info => info.Type == typeof(WhistleBuff) && info.Stacks % 3 == 0);
            }

            if (type == typeof(NymeiasWheel))
            {
                return craftTableInfo.Buffs.Any(info => info.Type == typeof(WhistleBuff) && info.Stacks > 0);
            }

            if (type == typeof(TrainedHand))
            {
                var innerQuiet = craftTableInfo.Buffs.FirstOrDefault(info => info.Type == typeof(InnerQuietBuff));
                var whistles = craftTableInfo.Buffs.FirstOrDefault(info => info.Type == typeof(WhistleBuff));
                return innerQuiet?.Stacks > 0 && whistles?.Stacks > 0 && innerQuiet.Stacks == whistles.Stacks;
            }

            if (type == typeof(MuscleMemory) || type == typeof(MakersMark))
            {
                return craftTableInfo.Step == 1;
            }
            return false;
        }
    }
}
