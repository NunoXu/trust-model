using HelpersForNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Util;

namespace TrustModel.Features
{
    public class Category : INotifyPropertyChanged, IKeyedResource<string>
    {
        private string _name = Guid.NewGuid().ToString();
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); NotifyPropertyChanged("Key"); } }

        [XmlIgnore]
        public string Key { get { return Name; } }

        [XmlIgnore]
        public bool Deleted { get; set; } = false;

        [XmlIgnore]
        public ObservableCollection<FeatureModel> Features
        {
            get
            {
                var features = Singleton<FeaturesManager>.Instance.Features.Map.Values;
                var result = features.Where(x => x.Category.Equals(this));
                return new ObservableCollection<FeatureModel>(result);
            }
        }

        public Category() { }

        public Category(string name)
        {
            _name = Name;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
