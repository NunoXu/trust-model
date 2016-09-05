using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Features.BeliefSources
{
    [Serializable]
    public class DirectContact : BeliefSource
    {
        
        [XmlIgnore]
        private DateTime _contactTime;
    
        [XmlElement("BeliefValue")]
        public override double BeliefValue { get; set; }

        [XmlElement("Certainty")]
        public override double Certainty { get; set; }

        public DirectContact() : base() { }

        public DirectContact(double beliefValue, double certainty) : this()
        {
            this.BeliefValue = beliefValue;
            this._contactTime = DateTime.Now;
            this.Certainty = certainty;
        }
    }
}
