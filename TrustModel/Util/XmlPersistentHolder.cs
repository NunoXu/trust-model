using HelpersForNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public class XmlPersistentHolder<T, O, M>
        where T: IKeyedResource<O>, INotifyPropertyChanged
        where M: ManagerSingleton<M, O, T>, new()
    {
        public XmlPersistentHolder() : base() {}

        public XmlPersistentHolder(T resource) : this()
        {
            Resource = resource;
        }

        [XmlElement("ResourceKey")]
        public O ResourceKey { get; set; }

        [XmlIgnore]
        private T _resource;

        [XmlIgnore]
        public virtual T Resource {
            get
            {
                if (_resource == null)
                {
                    _resource = Singleton<M>.Instance.ResourceMap[ResourceKey];
                    _resource.PropertyChanged += ResourceChanged;
                }
                return _resource;
            }
            set
            {
                _resource = value;
                _resource.PropertyChanged += ResourceChanged;
                ResourceKey = _resource.Key;
            }
        }

        private void ResourceChanged(object sender, PropertyChangedEventArgs e)
        {
            T resource = (T)sender;
            ResourceKey = resource.Key;
        }


    }
}
