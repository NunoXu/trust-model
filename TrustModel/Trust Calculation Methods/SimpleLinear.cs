﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;

namespace TrustModel.Trust_Calculation_Methods
{
    public class SimpleLinear : TrustFunction
    {
        public override double CalculateTrust(Agent trustor, Trustee trustee, Action action)
        {
            var actionFeatures = action.getWeightedFeatures();
            List<FeatureModel> looseFeatures = new List<FeatureModel>();

            double result = 0;
            foreach (FeatureModel feature in trustee.getFeatures())
            {
                if (actionFeatures.ContainsKey(feature))
                {
                    var weight = actionFeatures[feature];
                    result += weight * feature.beliefValue;
                }
                else
                {
                    looseFeatures.Add(feature);
                }
            }

            result *= 0.9d;

            foreach (FeatureModel looseFeature in looseFeatures)
            {
                result += (looseFeature.beliefValue / looseFeatures.Count) * 0.1;
            }

            return result;
        }
    }
}
