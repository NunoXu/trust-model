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

        public PerceptionsHolder Perceptions = new PerceptionsHolder();


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


    }
}
