using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustModel.Features
{
    class DirectContact : BeliefSource
    {

        private float _beliefValue;
        private DateTime _contactTime;

        public override double BeliefValue => _beliefValue;
        public override double Believability
        {
            get
            {
                return -Math.Log(-(_contactTime.Ticks/DateTime.Now.Ticks) + 1);
            }
        }
    }
}
