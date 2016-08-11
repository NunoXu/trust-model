using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;
using TrustModel.Util;

namespace TrustModel.Perceptions
{
    public class PerceptionsManager
    {
        [Serializable, XmlRoot("Perceptions"), XmlType("Perceptions")]
        public class PerceptionsHolder : XmlHolder<PerceptionsHolder>
        {
            [XmlElement("Perception")]
            public List<string> List = new List<string>();

        }

        public string PerceptionsFilePath;
        public PerceptionsHolder Perceptions;

        public PerceptionsManager(string _perceptionsFilePath)
        {
            PerceptionsFilePath = _perceptionsFilePath;
            LoadOrCreate();
        }

        public void LoadOrCreate()
        {
            Perceptions = PerceptionsHolder.LoadOrCreate(PerceptionsFilePath);
        }

        public void Load()
        {
            Perceptions = PerceptionsHolder.Load(PerceptionsFilePath);
        }

        public void Save()
        {
            Perceptions.Save(PerceptionsFilePath);
        }


    }
}
