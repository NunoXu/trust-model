using DrWPF.Windows.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrustModel.Trust_Calculation_Methods;
using TrustModel.Util;

namespace TrustModel
{
    [Serializable]
    public class Agent : INotifyPropertyChanged
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
            }
        }
        

        [XmlElement(Order = 2)]
        public SerializableDictionary<string, Trustee> Trustees { get; set; } = new SerializableDictionary<string, Trustee>();

        public Agent() : this("Joe") { }

        public Agent(string name)
        {
            this.Name = name;
            AddEventHandlers();
        }

        public Dictionary<string, double> TrusteeTrustValues(Agent targetAgent, Action action, TrustFunction[] trustFunctions)
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

            Trustees.Add(trustee.AgentName, trustee);
            trustee.Agent.PropertyChanged += OnAgentNameChanged;
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
        }

        public IReadOnlyDictionary<string, Trustee> GetTrustees()
        {
            return new ReadOnlyDictionary<string, Trustee>(Trustees);
        }


        private Trustee getTrustee(Agent targetAgent)
        {
            foreach (Trustee trustee in Trustees.Values)
            {
                if (trustee.Agent.Equals(targetAgent))
                {
                    return trustee;
                }
            }
            throw new TrusteeNotFoundException("Trustee " + targetAgent.Name + " not found in agent " + this.Name + ".");
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
            foreach (Trustee trustee in Trustees.Values)
                trustee.Agent.PropertyChanged += OnAgentNameChanged;
        }
        
    }
}
