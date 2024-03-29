﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features;
using TrustModel.Features.BeliefSources;
using TrustModel.Util;

namespace TrustModel.Perceptions
{
    [Serializable]
    public class PerceptionModel : INotifyPropertyChanged, IKeyedResource<string>
    {
        public enum BeliefType { Bias, DirectContact, Reputation };

        private string _name = "";

        [XmlElement]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Key");
            }
        }

        [XmlIgnore]
        public bool Deleted { get; set; } = false;

        [XmlIgnore]
        public string Key { get { return Name; } }

        public PerceptionModel()
        {
            _name = Guid.NewGuid().ToString();
        }

        public PerceptionModel(string name)
        {
            _name = name;
        }

        public XmlPersistentCollectionHolder<string, Agent, AgentsManager> AffectedAgents { get; set; } = new XmlPersistentCollectionHolder<string, Agent, AgentsManager>();
        public XmlPersistentCollectionHolder<string, Agent, AgentsManager> TargetTrustees { get; set; } = new XmlPersistentCollectionHolder<string, Agent, AgentsManager>();
        public XmlPersistentCollectionHolder<string, FeatureModel, FeaturesManager> FeaturesToSpawn { get; set; } = new XmlPersistentCollectionHolder<string, FeatureModel, FeaturesManager>();
        public BeliefType TypeOfBeliefSource { get; set; } = BeliefType.DirectContact;
        
        public Perception SpawnPerception(double beliefValue, double certainty)
        {
            return new Perception(this, beliefValue, certainty, AffectedAgents, TargetTrustees, FeaturesToSpawn, TypeOfBeliefSource);
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
