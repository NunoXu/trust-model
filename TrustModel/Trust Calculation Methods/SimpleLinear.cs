using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Actions;
using TrustModel.Features;

namespace TrustModel.Trust_Calculation_Methods
{
    public class SimpleLinear : TrustFunction
    {
        public override double CalculateTrust(Agent trustor, Trustee trustee, TrustAction action)
        {
            var actionFeatures = action.WeightedFeatures;
            List<Feature> looseFeatures = new List<Feature>();

            double result = 0;
            foreach (Feature feature in trustee.Features)
            {
                if (actionFeatures.Any(x => x.Key.Resource.Equals(feature.FeatureID.Resource)))
                {
                    var weight = actionFeatures.First(x => x.Key.Resource.Equals(feature.FeatureID.Resource)).Value;
                    result += weight * feature.BeliefValue;
                }
                else
                {
                    looseFeatures.Add(feature);
                }
            }

            result *= 0.9d;

            foreach (Feature looseFeature in looseFeatures)
            {
                result += (looseFeature.BeliefValue / looseFeatures.Count) * 0.1;
            }

            return result;
        }
    }
}
