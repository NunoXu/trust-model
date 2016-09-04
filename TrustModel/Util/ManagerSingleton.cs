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
using Utils;

namespace TrustModel.Util
{
    public abstract class ManagerSingleton<T, M, N> : INotifyPropertyChanged, IDisposable
        where T: ManagerSingleton<T, M, N>, new()
        where N: INotifyPropertyChanged, IKeyedResource<M>
    {
        [Serializable, XmlRoot("ResourceHolder"), XmlType("ResourceHolder")]
        public abstract class ResourceHolder<O> : XmlDictionaryHolder<O, M, N>
            where O : ResourceHolder<O>, new()
        {
            [XmlIgnore]
            public override abstract SerializableDictionary<M, N> Map { get; set; }

            public ResourceHolder() : base()
            {
                ((INotifyCollectionChanged)Map).CollectionChanged += OnMapChange;
            }

            public void Add(N value)
            {
                Map.Add(value.Key, value);
            }

            public void Remove(N value)
            {
                Map.Remove(value.Key);
            }

            private void OnMapChange(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems != null)
                {
                    foreach (object newItem in e.NewItems)
                    {
                        var element = ((KeyValuePair<M, N>)newItem).Value;
                        element.PropertyChanged += OnElementChange;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (object oldItem in e.OldItems)
                    {
                        var element = ((KeyValuePair<M, N>)oldItem).Value;
                        var map = (SerializableDictionary<M, N>)sender;
                        if (!map.ContainsKey(element.Key))
                        {
                            element.PropertyChanged -= OnElementChange;
                        }
                    }
                }
                Save();
            }

            private void OnElementChange(object sender, PropertyChangedEventArgs e)
            {
                var element = (N)sender;

                if (e.PropertyName == "Key")
                {
                    if (Map.Any(kvp => kvp.Value.Key.Equals(element.Key)))
                    {
                        var pair = Map.FirstOrDefault(kvp => kvp.Value.Key.Equals(element.Key));
                        Map.Remove(pair.Key);
                        Map[element.Key] = pair.Value;

                    }
                }
                Save();
            }
        }


        public abstract SerializableDictionary<M, N> ResourceMap { get; }
        
        protected ManagerSingleton(){}


        private string _filePath;
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            internal set
            {
                _filePath = value;
                InObjectLoad();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public abstract void LoadOrCreate();
        public abstract void Save();
        public abstract void Load();
        protected abstract void InObjectLoad();


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ManagerSingleton() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
