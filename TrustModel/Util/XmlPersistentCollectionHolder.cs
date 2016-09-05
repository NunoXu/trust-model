using HelpersForNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Util;
using Utils;

namespace TrustModel.Util
{
    public class XmlPersistentCollectionHolder<M, N, O> : XmlCollectionHolder<XmlPersistentCollectionHolder<M, N, O>, N>
        where N : INotifyPropertyChanged, IKeyedResource<M>
        where O : ManagerSingleton<O, M, N>, new()
    {
        [XmlIgnore]
        private ObservableCollection<N> _list;

        [XmlIgnore]
        public override ObservableCollection<N> List
        {
            get
            {
                if (_list == null)
                {
                    _list = new ObservableCollection<N>();
                    RetrieveTrueValues();
                }
                return _list;
            }
            set
            {
                _list = value;
            }
        }

        [XmlElement("Item")]
        public virtual ObservableCollection<M> KeyList { get; set; } = new ObservableCollection<M>();


        protected void RetrieveTrueValues()
        {
            foreach(M key in KeyList)
            {
                N value = Singleton<O>.Instance.ResourceMap[key];
                _list.Add(value);
                value.PropertyChanged += OnMapElementChanged;
            }
        }

        private void OnMapElementChanged(object sender, PropertyChangedEventArgs e)
        {
            ReBuildKeyList();
        }

        private void ReBuildKeyList()
        {
            KeyList.Clear();
            foreach(N element in List)
            {
                KeyList.Add(element.Key);
            }
        }
    }
}
