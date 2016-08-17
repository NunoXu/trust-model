using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrustModel.Util
{
    public abstract class XmlCollectionHolder<T , M>: XmlHolder<T>
        where T : XmlHolder<T>, new()
    {
        [XmlIgnore]
        public abstract ObservableCollection<M> List { get; set; }

        public XmlCollectionHolder()
        {
            List.CollectionChanged += OnCollectionChanged;
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Save();
        }
    }
}
