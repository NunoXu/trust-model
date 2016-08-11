using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Trust_Calculation_Methods;

namespace TrustModel
{
    public class Agent
    {
        public string name
        {
            get; protected set;
        }
        protected Dictionary<string, Trustee> trustees;


        public Agent(string name)
        {
            this.name = name;
            trustees = new Dictionary<string, Trustee>();
        }

        public Dictionary<string, double> TrusteeTrustValues(Agent targetAgent, Action action, TrustFunction[] trustFunctions)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            Trustee targetTrustee = trustees[targetAgent.name];
            
            foreach (TrustFunction trustFunction in trustFunctions)
            {
                result.Add(trustFunction.ToString(), trustFunction.CalculateTrust(this, targetTrustee, action));
            }

            return result;
        }


        public void AddTrustee(Trustee trustee)
        {
            trustees.Add(trustee.agent.name, trustee);
        }

       

        public IReadOnlyDictionary<string, Trustee> GetTrustees()
        {
            return new ReadOnlyDictionary<string, Trustee>(trustees);
        }


        private Trustee getTrustee(Agent targetAgent)
        {
            foreach (Trustee trustee in trustees.Values)
            {
                if (trustee.agent.Equals(targetAgent))
                {
                    return trustee;
                }
            }
            throw new TrusteeNotFoundException("Trustee " + targetAgent.name + " not found in agent " + this.name + ".");
        }
    }
}
