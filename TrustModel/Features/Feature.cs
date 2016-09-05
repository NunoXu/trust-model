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
using TrustModel.Features.BeliefSources;
using TrustModel.Util;

namespace TrustModel.Features
{
    [Serializable]
    public class Feature: INotifyPropertyChanged
    {
        [XmlElement("BeliefSource")]
        public ObservableCollection<BeliefSource> BeliefSources = new ObservableCollection<BeliefSource>();

        public XmlPersistentHolder<FeatureModel, string, FeaturesManager> FeatureID { get; set; }

        public Feature() : base() {
            ((INotifyCollectionChanged)BeliefSources).CollectionChanged += BeliefSourcesChanged;
        }

        private void BeliefSourcesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("BeliefSources");
        }

        public Feature (FeatureModel featureID) : this()
        {
            FeatureID = new XmlPersistentHolder<FeatureModel, string, FeaturesManager>(featureID);
        }

        public double BeliefValue
        {
            get
            {
                double totalBelief = 0;
                foreach (BeliefSource belief in BeliefSources)
                {
                    totalBelief += belief.BeliefValue * belief.Certainty;
                }

                return totalBelief / BeliefSources.Count;
            }
        }

        public void AddBeliefSource(BeliefSource source)
        {
            BeliefSources.Add(source);
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal void AddBeliefSources(ObservableCollection<BeliefSource> beliefSources)
        {
            foreach(BeliefSource beliefSource in beliefSources)
            {
                BeliefSources.Add(beliefSource);
            }
        }
    }
}
