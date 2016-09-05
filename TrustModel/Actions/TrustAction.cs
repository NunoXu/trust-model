using DrWPF.Windows.Data;
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
using TrustModel.Util;
using Utils;

namespace TrustModel.Actions
{
    [Serializable]
    public class TrustAction : INotifyPropertyChanged, IKeyedResource<string>
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [XmlIgnore]
        private string _name;


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
        public SerializableDictionary<XmlPersistentHolder<FeatureModel, string, FeaturesManager>, double> WeightedFeatures { get; set; }
            = new SerializableDictionary<XmlPersistentHolder<FeatureModel, string, FeaturesManager>, double>();

        public TrustAction()
        {
            ((INotifyCollectionChanged)WeightedFeatures).CollectionChanged += TrustAction_CollectionChanged;
        }

        private void TrustAction_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("WeightedFeatures");
        }

        public string Key
        {
            get
            {
                return Name;
            }
        }

        public bool Deleted { get; set; } = false;

        public TrustAction (string name)
        {
            Name = name;
        }

        
        public void AddFeature(FeatureModel feature, double weight)
        {
            WeightedFeatures.Add(new XmlPersistentHolder<FeatureModel, string, FeaturesManager>(feature), weight);
        }

    }
}
