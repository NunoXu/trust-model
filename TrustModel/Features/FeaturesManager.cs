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
using Utils;

namespace TrustModel.Features
{
    public class FeaturesManager : ManagerSingleton<FeaturesManager, string, FeatureModel>
    {
        [Serializable, XmlRoot("Features"), XmlType("Features")]
        public class FeaturesHolder : ResourceHolder<FeaturesHolder>
        {
            public override SerializableDictionary<string, FeatureModel> Map { get; set; } = new SerializableDictionary<string, FeatureModel>();
        }

        private FeaturesHolder _features = new FeaturesHolder();
        public FeaturesHolder Features
        {
            get
            {
                return _features;
            }
            private set
            {
                _features = value;
                NotifyPropertyChanged();
            }
        }

        public override SerializableDictionary<string, FeatureModel> ResourceMap
        {
            get
            {
                return Features.Map;
            }
        }

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

        protected override void InObjectLoad()
        {
            Features.InObjectLoad(FilePath);
        }
    }
}
