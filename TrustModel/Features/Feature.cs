using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features.BeliefSources;

namespace TrustModel.Features
{
    [Serializable]
    public class Feature : INotifyPropertyChanged
    {
        protected List<BeliefSource> beliefs = new List<BeliefSource>();
        protected List<Feature> childFeatures;
        protected List<Feature> parentFeatures;

        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); } }


        public Category Category { get; set; }
        public double beliefValue
        {
            get
            {
                double totalBelief = 0;
                foreach (BeliefSource belief in beliefs)
                {
                    totalBelief += belief.BeliefValue * belief.Certainty;
                }

                return totalBelief / beliefs.Count;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Feature ()
        {
            Name = Guid.NewGuid().ToString();
            Category = new Category();

        }

        public Feature (string name, Category category)
        {
            this.Name = name;
            this.Category = category;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Feature featureObj = (Feature)obj;
            if (featureObj.Name == this.Name && featureObj.Category == this.Category)
                return true;
            else
                return base.Equals(obj);
        }
        

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Category.GetHashCode();
        }


        public void AddBeliefSource(BeliefSource source)
        {
            beliefs.Add(source);
        }
    }
}
