using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CraftTable.Abilities;
using CraftTable.Abilities.Specialist;
using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable
{
    public class CraftStation
    {
        public static CraftTable CreateCraftTable(Recipe recipe, CraftMan craftMan, IProgressWatcher watcher = null)
        {
            var efficiencyCalculator = new EfficiencyCalculator();
            var lookupService = new LookupService();
            var calculator = new Calculator(efficiencyCalculator, lookupService);
            var buffCollector = new BuffCollector();
            var randomService = new RandomService();
            var condition = new ConditionService(randomService, calculator);
            
            
            var craftQualityCalculator = new CraftQualityCalculator();
            return new CraftTable(buffCollector, condition, randomService, calculator, lookupService, craftQualityCalculator, recipe, craftMan, watcher);
        }

        private static List<Ability> GetAbilities()
        {
            return  Assembly.GetExecutingAssembly().GetTypes().Where(type => type.BaseType == typeof(Ability) && type.GetCustomAttributes(typeof(AbilityDescriptorAttribute),false)?.Length>0).Select(type => (Ability)Activator.CreateInstance(type)).ToList();
        }

        public static Ability GetAbility(string name)
        {
            return GetAbilities().SingleOrDefault(ability => ability.Name() == name);
        }

        public static Overrides CreateOverride(Condition condition, bool isFailed)
        {
            return new Overrides() {Condition = condition, Failed = isFailed};
        }

        public static List<AbilityInfo> GetAbilitiesInfo(Crafter crafter)
        {
            return GetAbilities().Select(a => new AbilityInfo()
            {
                Name = a.Name(),
                XivDbId = a.IdForCrafter(crafter),
                Category = a.AbilityDescriptor().Category.ToString(),
                Order = a.AbilityDescriptor().Order,
                CraftPoints = a.AbilityDescriptor().CpCost,
                IsCrossClass = a.AbilityDescriptor().IsCrossClass,
                Crafter = a.AbilityDescriptor().CrafterAfinity
            }).ToList();
        }

        


        public static bool CheckHighLight(Ability ability, CraftTableInfo craftTableInfo)
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


        public static void CheckDistribution()
        {

            var calculator = new Calculator(new EfficiencyCalculator(), new LookupService());
            var conditionService = new ConditionService(new RandomService(), calculator);

            Condition? condition = null;
            IList<Condition> conditions = new List<Condition>();
            for (var i = 0; i < 100000; i++)
            {
                condition = conditionService.GetCondition(condition);
                conditions.Add(condition.Value);
            }

            var a = from c in conditions
                    group c by c
                into g
                    select new { Condition = g.Key, Count = g.Count(), Percent = (double)g.Count() / conditions.Count };
            Console.WriteLine($"Condition: \t Count \t\t %");
            foreach (var c in a.OrderBy(arg => arg.Condition))
            {
                Console.WriteLine($"{c.Condition}\t\t {c.Count} \t\t {c.Percent * 100}");
            }
        }


    }

    public class Watcher : IProgressWatcher
    {
        public List<string> Logs { get; set; } = new List<string>();

        public void Log(string s)
        {
            Logs.Add(s);
        }
    }

    public class AbilityInfo
    {
        public string Name { get; set; }
        public string XivDbId { get; set; }
        public string Category { get; set; }
        public int Order { get; set; }
        public int CraftPoints { get; set; }
        public bool IsCrossClass { get; set; }
        public Crafter Crafter { get; set; }

        public bool IsForCrafter(Crafter crafter)
        {
            return Crafter.HasFlag(crafter);
        }
    }
}