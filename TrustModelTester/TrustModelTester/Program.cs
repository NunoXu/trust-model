using HelpersForNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel;
using TrustModel.Features;

namespace TrustModelTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TrustModelManager model = new TrustModelManager();
            var bob = new Agent("BOB");
            Singleton<AgentsManager>.Instance.Agents.Add(bob);
            var ana = new Agent("Ana");
            Singleton<AgentsManager>.Instance.Agents.Add(ana);
            var t = new Trustee(ana);
            bob.AddTrustee(t);
            Singleton<AgentsManager>.Instance.Save();

            Singleton<FeaturesManager>.Instance.Features.Add(new FeatureModel("A", new Category()));
            Singleton<FeaturesManager>.Instance.Save();
            /*foreach(Feature feature in FeaturesManager.Instance.Features.List)
            {
                
            }*/
        }
    }
}
