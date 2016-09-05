using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Features.BeliefSources
{
    [Serializable]
    [XmlInclude(typeof(DirectContact)), XmlInclude(typeof(Bias)), XmlInclude(typeof(Reputation))]
    public abstract class BeliefSource
    {
        public abstract double BeliefValue { get; set; }
        public abstract double Certainty { get; set; }

        public BeliefSource() { }
    }
}
