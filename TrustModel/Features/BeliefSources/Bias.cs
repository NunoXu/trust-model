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
        private double _beliefValue;
        private double _certainty;
        private DateTime _contactTime;

        public override double BeliefValue => _beliefValue;
        public override double Certainty => _certainty;
    }
}
