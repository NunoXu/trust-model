using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;

namespace TrustModel
{
    public class Action
    {
        public string name { get; protected set; }
        protected Dictionary<FeatureModel, double> weightedFeatures;


        public Action (string name)
        {
            this.name = name;
            this.weightedFeatures = new Dictionary<FeatureModel, double>();
        }

        public IReadOnlyDictionary<FeatureModel, double> getWeightedFeatures()
        {
            return new ReadOnlyDictionary<FeatureModel, double>(weightedFeatures);
        }
        
        public void addFeature(FeatureModel feature, double weight)
        {
            weightedFeatures.Add(feature, weight);
        }

    }
}
