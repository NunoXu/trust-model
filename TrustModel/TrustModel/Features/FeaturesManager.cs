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

        public string FeaturesFilePath;
        public FeaturesHolder Features = new FeaturesHolder();

        public FeaturesManager(string filePath)
        {
            FeaturesFilePath = filePath;
            LoadOrCreate();
        }

        public void LoadOrCreate()
        {
            Features = FeaturesHolder.LoadOrCreate(FeaturesFilePath);
        }

        public void Load()
        {
            Features = FeaturesHolder.Load(FeaturesFilePath);
        }

        public void Save()
        {
            Features.Save(FeaturesFilePath);
        }
        
    }
}
