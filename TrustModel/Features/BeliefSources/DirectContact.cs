using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustModel.Features.BeliefSources
{
    public class DirectContact : BeliefSource
    {

        private double _beliefValue;
        private double _certainty;
        private DateTime _contactTime;
    
        public override double BeliefValue => _beliefValue;
        public override double Certainty => _certainty;

        public DirectContact(double beliefValue, double certainty)
        {
            this._beliefValue = beliefValue;
            this._contactTime = DateTime.Now;
            this._certainty = certainty;
        }
    }
}
