using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public abstract double CalculateTrust(Agent trustor, Trustee trustee, Action action);

        public override string ToString()
        {
            return Name;
        }
    }
}
