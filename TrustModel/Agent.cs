using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Trust_Calculation_Methods;

namespace TrustModel
{
    class Agent
    {
        protected IList<Trustee> trustees;
        protected IList<TrustFunction> trustFunctions;

        public double[] TrusteeTrustValues(Trustee trustee, Action action)
        {
            double[] result = new double[trustFunctions.Count];

            int i = 0;
            foreach (TrustFunction trustFunction in trustFunctions)
            {
                result[i++] = trustFunction.CalculateTrust();
            }

            return result;
        }

    }
}
