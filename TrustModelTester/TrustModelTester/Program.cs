using HelpersForNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel;
using TrustModel.Actions;
using TrustModel.Features;
using TrustModel.Perceptions;
using TrustModel.Trust_Calculation_Methods;

namespace TrustModelTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TrustModelManager model = new TrustModelManager();
            /*var ana = new Agent("Ana");
            Singleton<AgentsManager>.Instance.Agents.Add(ana);
            var bob = new Agent("Bob");
            Singleton<AgentsManager>.Instance.Agents.Add(bob);

            var cat = new Category("Ability");
            Singleton<CategoriesManager>.Instance.Categories.Add(cat);

            var feature = new FeatureModel("Cooking", cat);
            Singleton<FeaturesManager>.Instance.Features.Add(feature);

            var perceptionModel = new PerceptionModel("Saw Cooking");
            perceptionModel.AffectedAgents.List.Add(ana);
            perceptionModel.TargetTrustees.List.Add(bob);
            perceptionModel.FeaturesToSpawn.List.Add(feature);

            var perception = perceptionModel.SpawnFeature(10, 1);
            perception.UpdateModel();
            
            */ 
            Agent ana = Singleton<AgentsManager>.Instance.Agents["Ana"];
            Agent bob = Singleton<AgentsManager>.Instance.Agents["Bob"];

            var feature = Singleton<FeaturesManager>.Instance.Features["Cooking"];
            TrustAction action = new TrustAction("Cooking");
            action.AddFeature(feature, 1);

            var a = ana.TrusteeTrustValues(bob, action,  new[] { new SimpleLinear() } );
            
        }
    }
}
