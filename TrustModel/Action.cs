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
        protected Dictionary<Feature, double> weightedFeatures;


        public Action (string name)
        {
            this.name = name;
            this.weightedFeatures = new Dictionary<Feature, double>();
        }

        public IReadOnlyDictionary<Feature, double> getWeightedFeatures()
        {
            return new ReadOnlyDictionary<Feature, double>(weightedFeatures);
        }
        
        public void addFeature(Feature feature, double weight)
        {
            weightedFeatures.Add(feature, weight);
        }

    }
}
