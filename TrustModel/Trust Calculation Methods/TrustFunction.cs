using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Actions;

namespace TrustModel.Trust_Calculation_Methods
{
    public abstract class TrustFunction
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }


        public abstract double CalculateTrust(Agent trustor, Trustee trustee, TrustAction action);

        public override string ToString()
        {
            return Name;
        }
    }
}
