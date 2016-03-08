using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustModel.Features
{
    abstract class Feature
    {
        protected string name;
        protected string category;

        public Feature (string name, string category)
        {
            this.name = name;
            this.category = category;
        }

        protected abstract double BeliefValue();
    }
}
