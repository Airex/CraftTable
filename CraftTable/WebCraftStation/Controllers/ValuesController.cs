using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.UI.WebControls;
using CraftTable;
using CraftTable.Exceptions;

namespace WebCraftStation.Controllers
{
    public class Request
    {
        public int Iterations { get; set; } = 1;
        public List<string> Abilities { get; set; }
    }

    public enum ProcessState
    {
        NotStarted,
        Success,
        NotFinished,
        Failed
    }

    public class ProcessStatus
    {
        public ProcessState State { get; set; } = ProcessState.NotStarted;
    }

    public class Response
    {
        public IDictionary<ProcessState, Statistics> Statistics { get; set; }
    }

    public class Statistics
    {
        public int Total { get; set; }
        public int AbilitiesFailed { get; set; }
        public int NumberOfHighQuality { get; set; }
    }

    public class ValuesController : ApiController
    {
        private readonly CraftTable.CraftTable.Factory _factory;
        private readonly IEnumerable<Ability> _abilities;

        public ValuesController(CraftTable.CraftTable.Factory factory, IEnumerable<Ability> abilities)
        {
            _factory = factory;
            _abilities = abilities;
        }

        
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK,"Ok");
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]Request value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Abilities == null) throw new ArgumentNullException(nameof(value.Abilities));

            var craftMan = new CraftMan(Crafter.Culinarian, 788, 851, 346, 60);
            var recipe = new Recipe(478, 60, 3140, 150);

            var processStates = Enumerable.Range(1,Math.Min(100,value.Iterations)).Select(i => Process(value.Abilities, recipe, craftMan)).ToList();

            var a = from p in processStates
                group p by p.ProcessState
                into g
                select g;
                

            var statistics = a.ToDictionary(arg => arg.Key, arg => 
                new Statistics()
                {
                    Total = arg.Count(),
                    AbilitiesFailed = arg.Sum(status => status.FailedAbilitie),
                    NumberOfHighQuality = arg.Sum(status => status.IsHighQuality?1:0)
                });

            return Json(new Response() { Statistics = statistics});

        }

        public class ProcessStatus
        {
            public ProcessState ProcessState { get; set; }
            public int FailedAbilitie { get; set; }
            public bool IsHighQuality { get; set; }
        }

        private ProcessStatus Process(IEnumerable<string> abilities, Recipe recipe, CraftMan craftMan)
        {
            int abilitiesFailed = 0;
            bool isHightQuality = false;

            var craftTable = _factory(recipe, craftMan);

            var result = ProcessState.NotStarted;

            foreach (var ability in abilities)
            {
                var firstOrDefault = _abilities.FirstOrDefault(a => a.Name() == ability);
                if (firstOrDefault == null)
                    throw new InvalidOperationException($"Ability {ability} was not found.");
                try
                {
                    craftTable.Act(firstOrDefault);
                }
                catch (CraftSuccessException ex)
                {
                    isHightQuality = ex.IsHighQuality;
                    result = ProcessState.Success;
                }
                catch (CraftFailedException)
                {
                    result = ProcessState.Failed;
                }
                catch (AbilityFailedException)
                {
                    abilitiesFailed++;
                }
                catch (AbilityNotAvailableException)
                {
                }
                catch (CraftAlreadyFinishedException)
                {
                    
                }
                catch (Exception ex)
                {
                    InternalServerError(ex);
                }
                if (result != ProcessState.NotStarted)
                {
                    break;
                }
            }
            if (result == ProcessState.NotStarted)
                result = ProcessState.NotFinished;

            return new ProcessStatus() {ProcessState = result,FailedAbilitie = abilitiesFailed, IsHighQuality = isHightQuality};
        }
    }
}
