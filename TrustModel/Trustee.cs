using HelpersForNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;

namespace TrustModel
{
    [Serializable]
    public class Trustee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        [XmlElement("Feature")]
        public ObservableCollection<Feature> Features { get; set; } = new ObservableCollection<Feature>();
        

        [XmlIgnore]
        [NonSerialized]
        private Agent _agent;

        [XmlIgnore]
        public Agent Agent
        {
            get
            {
                if (_agent == null)
                    Agent = Singleton<AgentsManager>.Instance.Agents[AgentName];
                return _agent;
            }

            set
            {
                _agent = value;
                AgentName = _agent.Name;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public Trustee() { }
        public Trustee (Agent agent)
        {
            AgentName = agent.Name;

            ((INotifyCollectionChanged)Features).CollectionChanged += FeaturesChanged;
        }

        private void FeaturesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Trustees");
        }

        public void UpdateFeature(Feature feature)
        {
            Feature targetFeature;
            if (!Features.Contains(feature, new FeatureComparer())) {
                Features.Add(feature);
                targetFeature = feature;
            } else
            {
                targetFeature = Features.FirstOrDefault(x => x.FeatureID.Equals(feature.FeatureID));
                targetFeature.AddBeliefSources(feature.BeliefSources);
            }

        }

        public void AddFeatures(Feature feature)
        {
            Features.Add(feature);
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
