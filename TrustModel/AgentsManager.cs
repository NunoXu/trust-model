using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Util;

namespace TrustModel
{
    public class AgentsManager : ManagerSingleton<AgentsManager>
    {
        [Serializable, XmlRoot("Agents"), XmlType("Agents")]
        public class AgentsHolder : XmlCollectionHolder<AgentsHolder, Agent>
        {
            [XmlElement("Agent")]
            public override ObservableCollection<Agent> List { get; set; } = new ObservableCollection<Agent>();

            public Agent GetByName(string name)
            {
                return List.FirstOrDefault(agent => agent.Name == name);
            }
        }
        
        public AgentsHolder Agents = new AgentsHolder();


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
