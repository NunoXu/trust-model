using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;
using TrustModel.Util;
using static TrustModel.Perceptions.PerceptionModel;

namespace TrustModel.Perceptions
{
    public class Perception : INotifyPropertyChanged
    {
        public PerceptionModel PerceptionId { get; set; }

        public Perception() : base() { }

        public Perception(PerceptionModel perceptionId) : this()
        {
            PerceptionId = perceptionId;
        }

        public Perception(PerceptionModel perceptionId, double beliefValue, double certainty) : this(perceptionId)
        {
            BeliefValue = beliefValue;
            Certainty = certainty;
        }

        [XmlIgnore]
        private double _beliefValue;

        [XmlIgnore]
        private double _certainty;

        public double BeliefValue
        {
            get
            {
                return _beliefValue;
            }
            set
            {
                _beliefValue = value;
                NotifyPropertyChanged();
            }
        }
        public double Certainty
        {
            get
            {
                return _certainty;
            }
            set
            {
                _certainty = value;
                NotifyPropertyChanged();
            }
        }


        public XmlPersistentCollectionHolder<string, Agent, AgentsManager> AffectedAgents { get; set; }
        public XmlPersistentCollectionHolder<string, Agent, AgentsManager> TargetTrustees { get; set; }
        public XmlPersistentCollectionHolder<string, FeatureModel, FeaturesManager> FeaturesToSpawn { get; set; }
        public BeliefType TypeOfBeliefSource { get; set; }

        public void UpdateModel()
        {
            foreach (Agent agent in AffectedAgents.List)
            {
                List<Feature> features = new List<Feature>();
                foreach (FeatureModel featureModel in FeaturesToSpawn.List)
                {
                    var feature = featureModel.GetAgentFeature();

                    /* Quick fix, belief source should be vary between DirectContact and Reputation */
                    var beliefSource = new DirectContact(BeliefValue, Certainty);
                    feature.AddBeliefSource(beliefSource);
                    features.Add(feature);
                }
                agent.UpdateTrustees(TargetTrustees.List, features);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
