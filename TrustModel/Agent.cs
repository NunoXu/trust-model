using DrWPF.Windows.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Actions;
using TrustModel.Features;
using TrustModel.Trust_Calculation_Methods;
using TrustModel.Util;
using Utils;

namespace TrustModel
{
    [Serializable]
    public class Agent : INotifyPropertyChanged, IKeyedResource<string>
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string _name = "";

        [XmlElement(Order = 1)] 
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("Key");
            }
        }

        [XmlIgnore]
        public string Key { get { return Name; } }


        [XmlElement(Order = 2)]
        public SerializableDictionary<string, Trustee> Trustees { get; set; } = new SerializableDictionary<string, Trustee>();

        [XmlIgnore]
        public bool Deleted { get; set; } = false;

        public Agent() : this(Guid.NewGuid().ToString()) { }

        public Agent(string name)
        {
            this.Name = name;
            AddEventHandlers();
        }

        public Dictionary<string, double> TrusteeTrustValues(Agent targetAgent, TrustAction action, TrustFunction[] trustFunctions)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            Trustee targetTrustee = Trustees[targetAgent.Name];
            
            foreach (TrustFunction trustFunction in trustFunctions)
            {
                result.Add(trustFunction.ToString(), trustFunction.CalculateTrust(this, targetTrustee, action));
            }

            return result;
        }


        public void AddTrustee(Trustee trustee)
        {
            Trustees.Add(trustee.Agent.Key, trustee);
            trustee.PropertyChanged += OnAgentNameChanged;
        }

        private void OnAgentNameChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                var agent = (Agent)sender;

                if (Trustees.Any(kvp => kvp.Value.Agent.Name == agent.Name))
                {
                    var item = Trustees.FirstOrDefault(kvp => kvp.Value.Agent.Name == agent.Name);
                    var trustee = item.Value;
                    Trustees.Remove(item.Key);
                    AddTrustee(trustee);
                }
            }
            OnPropertyChanged("Trustee");
        }

        public IReadOnlyDictionary<string, Trustee> GetTrustees()
        {
            return new ReadOnlyDictionary<string, Trustee>(Trustees);
        }


        private Trustee GetTrustee(Agent targetAgent)
        {
            foreach (Trustee trustee in Trustees.Values)
            {
                if (trustee.Agent.Equals(targetAgent))
                {
                    return trustee;
                }
            }
            return null;
        }

        private void PopulateTrustees(ObservableCollection<Trustee> list)
        {
            Trustees.Clear();
            foreach (Trustee trustee in list)
            {
                Trustees[trustee.Agent.Name] = trustee;
            }
        }
        

        private void AddEventHandlers()
        {
            ((INotifyCollectionChanged)Trustees).CollectionChanged += TrusteesChanged;

            foreach (Trustee trustee in Trustees.Values)
                trustee.PropertyChanged += OnAgentNameChanged;
        }

        private void TrusteesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Trustees");
        }

        public void UpdateTrustees(IEnumerable<Agent> trustees, IEnumerable<Feature> features)
        {
            foreach(Agent trustee in trustees)
            {
                var targetTrustee = GetTrustee(trustee);
                if (targetTrustee == null)
                {
                    targetTrustee = new Trustee(trustee);
                    AddTrustee(targetTrustee);
                }
                foreach (Feature feature in features)
                {
                    targetTrustee.UpdateFeature(feature);
                }
            }
        }
    }
}
