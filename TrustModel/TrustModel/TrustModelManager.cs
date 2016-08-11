using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;
using TrustModel.Perceptions;
using TrustModel.Util;

namespace TrustModel.TrustModel
{
    public class TrustModelManager
    {
        public class TrustModelSettings : XmlHolder<TrustModelSettings>
        {
            public string CategoriesXmlFilePath = "Resources/categories.xml";
            public string FeaturesXmlFilePath = "Resources/features.xml";
            public string PerceptionsXmlFilePath = "Resources/perceptions.xml";
        }

        private string _settingsFilePath = "Resources/modelSettings.xml";
        private TrustModelSettings modelSettings;

        public CategoriesManager categoriesManager;
        public FeaturesManager featuresManager;
        public PerceptionsManager perceptionsManager;


        public TrustModelManager()
        {
            modelSettings = TrustModelSettings.LoadOrCreate(_settingsFilePath);
            LoadManagers();
        }

        public TrustModelManager(string settingsFilePath) : this()
        {
            _settingsFilePath = settingsFilePath;
        }


        private void LoadManagers()
        {
            categoriesManager = new CategoriesManager(modelSettings.CategoriesXmlFilePath);
            featuresManager = new FeaturesManager(modelSettings.FeaturesXmlFilePath);
            perceptionsManager = new PerceptionsManager(modelSettings.PerceptionsXmlFilePath);
        }
    }
}
