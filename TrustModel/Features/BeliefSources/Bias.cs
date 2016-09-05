using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features.BeliefSources;

namespace TrustModel.Features.BeliefSources
{
    public class Bias : BeliefSource
    {
        
        private DateTime _contactTime;

        public override double BeliefValue { get; set; }
        public override double Certainty { get; set; }

        public Bias() : base() { }
        public Bias(double beliefValue, double certainty) :this()
        {
            BeliefValue = beliefValue;
            Certainty = certainty;
        }
    }
}
