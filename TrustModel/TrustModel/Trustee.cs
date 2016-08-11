using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;

namespace TrustModel
{
    public class Trustee
    {
        protected List<Feature> features = new List<Feature>();

        public Agent agent { get; protected set; }

        public Trustee (Agent agent)
        {
            this.agent = agent;
        }

        public void addFeatures(Feature feature)
        {
            features.Add(feature);
        }

        public IList<Feature> getFeatures()
        {
            return features.AsReadOnly();
        }
    }
}
