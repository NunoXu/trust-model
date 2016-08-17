using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel;

namespace TrustModelTester
{
    class Program
    {
        static void Main(string[] args)
        {
            TrustModelManager model = new TrustModelManager();
            var bob = new Agent("BOB");
            AgentsManager.Instance.Agents.List.Add(bob);
            var ana = new Agent("Ana");
            AgentsManager.Instance.Agents.List.Add(ana);
            var t = new Trustee(ana);
            bob.Trustees.Add(t.Agent.Name, t);
            AgentsManager.Instance.Save();
        }
    }
}
