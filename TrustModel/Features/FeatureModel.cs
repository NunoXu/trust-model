using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TrustModel.Features.BeliefSources;
using TrustModel.Util;

namespace TrustModel.Features
{
    [Serializable]
    public class FeatureModel : INotifyPropertyChanged, IKeyedResource<string>
    {
        protected List<FeatureModel> childFeatures = new List<FeatureModel>();
        protected List<FeatureModel> parentFeatures = new List<FeatureModel>();

        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged(); NotifyPropertyChanged("Key"); } }
        [XmlIgnore]
        public bool Deleted { get; set; } = false;


        public XmlPersistentHolder<Category, string, CategoriesManager> Category { get; set; }
        

        [XmlIgnore]
        public string Key { get { return Name; } }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public FeatureModel ()
        {
            Name = Guid.NewGuid().ToString();
            Category = new XmlPersistentHolder<Category, string, CategoriesManager>(new Category());

        }

        public FeatureModel (string name, Category category)
        {
            this.Name = name;
            this.Category = new XmlPersistentHolder<Category, string, CategoriesManager>(category);
        }

        public Feature GetAgentFeature()
        {
            return new Feature(this);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            FeatureModel featureObj = (FeatureModel)obj;
            if (featureObj.Name == this.Name && featureObj.Category == this.Category)
                return true;
            else
                return base.Equals(obj);
        }
        

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Category.GetHashCode();
        }



        
    }
}
