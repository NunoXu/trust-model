using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public abstract class XmlCollectionHolder<T , M>: XmlHolder<T>
        where T : XmlCollectionHolder<T, M>, new()
        where M : INotifyPropertyChanged
    {
        [XmlIgnore]
        public abstract ObservableCollection<M> List { get; set; }

        private bool InObjectLoading = false;
        private static object _lock = new object();

        public XmlCollectionHolder()
        {
            List.CollectionChanged += OnCollectionChanged;
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(!InObjectLoading)
                Save();
        }

        public void InObjectLoad(string filePath)
        {
            lock (_lock)
            {
                InObjectLoading = true;
                _lastFilePath = filePath;
                var loadedObject = LoadOrCreate(filePath);
                List.Clear();

                foreach (M item in loadedObject.List)
                {
                    List.Add(item);
                    item.PropertyChanged += Item_PropertyChanged;
                }
                
                InObjectLoading = false;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!InObjectLoading)
                Save();
        }
    }
}
