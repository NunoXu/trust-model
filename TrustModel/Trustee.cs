using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;

namespace TrustModel
{
    [Serializable]
    public class Trustee
    {
        public List<Feature> features = new List<Feature>();

        [XmlIgnore]
        [NonSerialized]
        private Agent _agent;

        [XmlIgnore]
        public Agent Agent
        {
            get
            {
                if (_agent == null)
                    _agent = AgentsManager.Instance.Agents.GetByName(AgentName);
                return _agent;
            }

            set
            {
                _agent = value;
                AgentName = _agent.Name;
            }
        }

        private string _agentName;
        public string AgentName {
            get
            {
                if (_agent == null)
                    return _agentName;
                else
                    return Agent.Name;
            }
            set
            {
                _agentName = value;
            }
        }

        public Trustee() { }
        public Trustee (Agent agent)
        {
            AgentName = agent.Name;
        }

        public void addFeatures(Feature feature)
        {
            features.Add(feature);
        }

        public IList<Feature> getFeatures()
        {
            return features.AsReadOnly();
        }
    }
}
