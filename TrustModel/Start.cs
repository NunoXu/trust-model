using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;
using TrustModel.Trust_Calculation_Methods;

namespace TrustModel
{
    class StartUI
    {
        static void Main(string[] args)
        {

            CategoriesManager fl = new CategoriesManager();
            Feature a = new Feature("Game Ability", "Ability");
            //fl.Categories.List.Add(new CategoriesManager.Cate());
            //fl.Categories.List.Add("Ability2");
            fl.Load("a.xml");

            //Simulation.run();
            /*
            Agent agentA = new Agent("Anna");
            Agent agentB = new Agent("Bob");

            Agent agentR = new Agent("Roboto");


            agentA.AddTrustee(new Trustee(agentR));
            Feature feat = new Feature("cooking", "skill");
            feat.AddBeliefSource(new DirectContact(0.8d, 0.9d));
            agentA.GetTrustees()[agentR.name].addFeatures(feat);

            TrustFunction[] trustFunctions = { new SimpleLinear() };
            Action action = new Action("Cook");
            action.addFeature(feat, 0.5);

            Dictionary<string, double> result = agentA.TrusteeTrustValues(agentR, action, trustFunctions);
            foreach (KeyValuePair<string, double> entry in result)
            {
                Debug.WriteLine(entry.Key + ": " + entry.Value);
            }*/
        }
    }
}
