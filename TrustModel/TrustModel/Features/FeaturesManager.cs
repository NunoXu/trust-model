using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;
using TrustModel.Util;

namespace TrustModel.Features
{
    public class FeaturesManager
    {
        [Serializable, XmlRoot("Features"), XmlType("Features")]
        public class FeaturesHolder : XmlHolder<FeaturesHolder>
        {
            [XmlElement("Feature")]
            public List<string> List = new List<string>();

        }

        public FeaturesHolder Features = new FeaturesHolder();

        public void Load(string filePath)
        {
            Features = FeaturesHolder.Load(filePath);
        }

        public void Save(string filePath)
        {
            Features.Save(filePath);
        }
        
    }
}
