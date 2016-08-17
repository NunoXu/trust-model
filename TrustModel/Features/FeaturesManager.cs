using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class FeaturesManager : ManagerSingleton<FeaturesManager>
    {
        [Serializable, XmlRoot("Features"), XmlType("Features")]
        public class FeaturesHolder : XmlCollectionHolder<FeaturesHolder, Feature>
        {
            [XmlElement("Feature")]
            public override ObservableCollection<Feature> List { get; set; } = new ObservableCollection<Feature>();
        }

    

        public FeaturesHolder Features = new FeaturesHolder();


        public override void LoadOrCreate()
        {
            Features = FeaturesHolder.LoadOrCreate(FilePath);
        }

        public override void Load()
        {
            Features = FeaturesHolder.Load(FilePath);
        }

        public override void Save()
        {
            Features.Save(FilePath);
        }
        
    }
}
