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
        public class PerceptionsHolder : XmlCollectionHolder<PerceptionsHolder, string>
        {
            [XmlElement("Perception")]
            public override ObservableCollection<string> List { get; set; } = new ObservableCollection<string>();
        }

        public PerceptionsHolder Perceptions;


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
