using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Util;
using Utils;

namespace TrustModel
{
    public class AgentsManager : ManagerSingleton<AgentsManager, string, Agent>
    {
        [Serializable, XmlRoot("Agents"), XmlType("Agents")]
        public class AgentsHolder : ResourceHolder<AgentsHolder>
        {
            public override SerializableDictionary<string, Agent> Map { get; set; } = new SerializableDictionary<string, Agent>();
        }



        public AgentsHolder Agents = new AgentsHolder();

        public override SerializableDictionary<string, Agent> ResourceMap
        {
            get
            {
                return Agents.Map;
            }
            
        }

        public override void LoadOrCreate()
        {
            Agents = AgentsHolder.LoadOrCreate(FilePath);
        }

        public override void Load()
        {
            Agents = AgentsHolder.Load(FilePath);
        }

        public override void Save()
        {
            Agents.Save(FilePath);
        }

        protected override void InObjectLoad()
        {
            Agents.InObjectLoad(FilePath);
        }
    }
}
