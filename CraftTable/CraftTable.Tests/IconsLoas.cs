using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Autofac;
using CraftTable.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CraftTable.Tests
{
//    [Explicit]
//    [Ignore("Integration")]
    public class IconsLoas
    {
        [Test]
        public void Load()
        {
            var output = "h:\\temp\\";
            string path = "http://api.xivdb.com/action/";
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new RegistrationModule());
            var container = builder.Build();
            var abilities = container.Resolve<IEnumerable<Ability>>();
            WebClient client = new WebClient();
            foreach (var ability in abilities)
            {
                var abilityXivDbAttributes = ability.GetType().GetCustomAttributes(typeof(AbilityXivDbAttribute),false).Cast<AbilityXivDbAttribute>();
                foreach (var abilityXivDbAttribute in abilityXivDbAttributes)
                {
                    var downloadString = client.DownloadString(path+abilityXivDbAttribute.AbilityId);
                    JObject value = (JObject) JsonConvert.DeserializeObject(downloadString);
                    var jToken = value["icon_hq"].Value<string>();
                    if (!Directory.Exists(output)) Directory.CreateDirectory(output);
                    client.DownloadFile(jToken,$"{output}action_{abilityXivDbAttribute.AbilityId}.png");
                }
                
            }
        }

        [Test]
        public void LoadBuffs()
        {
            var output = "h:\\temp\\Buffs\\";
            if (!Directory.Exists(output)) Directory.CreateDirectory(output);

            string path = "http://api.xivdb.com/status/";

            var abilities = typeof(IBuff).Assembly.GetTypes()
                .Where(type => typeof(IBuff).IsAssignableFrom(type))
                .SelectMany(
                    type => type.GetCustomAttributes(typeof(BuffXivDbAttribute), false).Cast<BuffXivDbAttribute>());
               
            
            
            WebClient client = new WebClient();
            foreach (var ability in abilities)
            {
                    var downloadString = client.DownloadString(path + ability.BuffId);
                    JObject value = (JObject)JsonConvert.DeserializeObject(downloadString);
                    var jToken = value["icon_hq"].Value<string>();
                    client.DownloadFile(jToken, $"{output}status_{ability.BuffId}.png");
             

            }
        }
    }
}