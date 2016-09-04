using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Features.BeliefSources;

namespace TrustModel.Features
{
    public class Feature: INotifyPropertyChanged
    {
        public ObservableCollection<BeliefSource> BeliefSources = new ObservableCollection<BeliefSource>();

        public FeatureModel FeatureID { get; set; }

        public Feature (FeatureModel featureID)
        {
            FeatureID = featureID;
        }

        public double beliefValue
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
                beliefSources.Add(beliefSource);
            }
        }
    }
}
