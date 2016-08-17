using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features.BeliefSources;

namespace TrustModel.Features
{
    [Serializable]
    public class Feature
    {
        protected List<BeliefSource> beliefs = new List<BeliefSource>();
        protected List<Feature> childFeatures;
        protected List<Feature> parentFeatures;

        public string name { get; set; }
        public string category { get; set; }
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

        public Feature ()
        {
            name = "Ability A";
            category = "Ability";

        }

        public Feature (string name, string category)
        {
            this.name = name;
            this.category = category;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Feature featureObj = (Feature)obj;
            if (featureObj.name == this.name && featureObj.category == this.category)
                return true;
            else
                return base.Equals(obj);
        }
        

        public override int GetHashCode()
        {
            return name.GetHashCode() + category.GetHashCode();
        }


        public void AddBeliefSource(BeliefSource source)
        {
            beliefs.Add(source);
        }
    }
}
