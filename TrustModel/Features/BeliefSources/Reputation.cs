using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;

namespace TrustModel.Features.BeliefSources
{
    public class Reputation : BeliefSource
    {
        public Trustee trustee;

        public override double BeliefValue { get; set; }
        public override double Certainty { get; set; }
    }
}
