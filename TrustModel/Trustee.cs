using HelpersForNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;

namespace TrustModel
{
    [Serializable]
    public class Trustee
    {
        public ObservableCollection<Feature> features { get; set; } = new ObservableCollection<Feature>();
        

        [XmlIgnore]
        [NonSerialized]
        private Agent _agent;

        [XmlIgnore]
        public Agent Agent
        {
            get
            {
                if (_agent == null)
                    _agent = Singleton<AgentsManager>.Instance.Agents[AgentName];
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

        public void UpdateFeature(Feature feature)
        {
            Feature targetFeature;
            if (!features.Contains(feature, new FeatureComparer())) {
                features.Add(feature);
                targetFeature = feature;
            } else
            {
                targetFeature = features.FirstOrDefault(x => x.FeatureID.Equals(feature.FeatureID));
            }

            targetFeature.AddBeliefSources(feature.BeliefSources);
        }

        public void AddFeatures(Feature feature)
        {
            features.Add(feature);
        }

        private class FeatureComparer : IEqualityComparer<Feature>
        {
            // Features are equal if their FeaturesID are equal.
            public bool Equals(Feature x, Feature y)
            {

                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the features' properties are equal.
                return x.FeatureID.Equals(y.FeatureID);
            }

            // If Equals() returns true for a pair of objects 
            // then GetHashCode() must return the same value for these objects.

            public int GetHashCode(Feature feature)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(feature, null)) return 0;

                return feature.FeatureID.GetHashCode();
            }

        }
    }
}
