using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;
using TrustModel.Util;
using Utils;

namespace TrustModel.Perceptions
{
    public class PerceptionsManager : ManagerSingleton<PerceptionsManager, string, PerceptionModel>
    {
        [Serializable, XmlRoot("Perceptions"), XmlType("Perceptions")]
        public class PerceptionsHolder : ResourceHolder<PerceptionsHolder>
        {
            public override SerializableDictionary<string, PerceptionModel> Map { get; set; } = new SerializableDictionary<string, PerceptionModel>();
        }

        private PerceptionsHolder _perceptions = new PerceptionsHolder();

        public PerceptionsHolder Perceptions
        {
            get
            {
                return _perceptions;
            }
            private set
            {
                _perceptions = value;
                NotifyPropertyChanged();
            }
        }

        public override SerializableDictionary<string, PerceptionModel> ResourceMap
        {
            get
            {
                return _perceptions.Map;
            }
        }

        public override void LoadOrCreate()
        {
            Perceptions = PerceptionsHolder.LoadOrCreate(FilePath);
            
        }

        public override void Load()
        {
            Perceptions = PerceptionsHolder.Load(FilePath);
        }

        public override void Save()
        {
            Perceptions.Save(FilePath);
        }

        protected override void InObjectLoad()
        {
            Perceptions.InObjectLoad(FilePath);
        }
    }
}
