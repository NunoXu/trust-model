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

namespace TrustModel.Perceptions
{
    public class PerceptionsManager : ManagerSingleton<PerceptionsManager>
    {
        [Serializable, XmlRoot("Perceptions"), XmlType("Perceptions")]
        public class PerceptionsHolder : XmlCollectionHolder<PerceptionsHolder, Perception>
        {
            [XmlElement("Perception")]
            public override ObservableCollection<Perception> List { get; set; } = new ObservableCollection<Perception>();
        }

        private PerceptionsHolder _perceptions = new PerceptionsHolder();

        public PerceptionsHolder Perceptions
        {
            get
            {
                return _perceptions;
            }
            set
            {
                _perceptions = value;
                NotifyPropertyChanged();
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
