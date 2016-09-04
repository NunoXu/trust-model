using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utils;

namespace TrustModel.Util
{
    public abstract class XmlDictionaryHolder<T, M, N> : XmlHolder<T>
        where T : XmlDictionaryHolder<T, M, N>, new()
        where N : INotifyPropertyChanged
    {
        [XmlIgnore]
        public abstract SerializableDictionary<M, N> Map { get; set; }

        private bool InObjectLoading = false;
        private static object _lock = new object();

        public XmlDictionaryHolder()
        {
            ((INotifyCollectionChanged)Map).CollectionChanged+= OnCollectionChanged;
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!InObjectLoading)
                Save();
        }

        public void InObjectLoad(string filePath)
        {
            lock (_lock)
            {
                InObjectLoading = true;
                _lastFilePath = filePath;
                var loadedObject = LoadOrCreate(filePath);
                Map.Clear();

                List<M> keys = new List<M>(loadedObject.Map.Keys);
                foreach (M key in keys)
                {
                    var item = loadedObject.Map[key];
                    Map.Add(key, item); 
                    item.PropertyChanged += Item_PropertyChanged;
                }

                InObjectLoading = false;
            }
        }

        public virtual N this[M key]
        {
            get { return Map[key]; }
            set { Map[key] = value; }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!InObjectLoading)
                Save();
        }

        
    }
}
