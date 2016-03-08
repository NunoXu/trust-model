using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;

namespace TrustModel
{
    class Trustee
    {
        protected IList<Feature> features;

        public void addFeatures(Feature feature)
        {
            features.Add(feature);
        }
    }
}
