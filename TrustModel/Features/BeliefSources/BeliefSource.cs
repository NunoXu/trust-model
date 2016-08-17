using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustModel.Features.BeliefSources
{
    public abstract class BeliefSource
    {
        public abstract double BeliefValue { get; }
        public abstract double Certainty { get; }        
    }
}
