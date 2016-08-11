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
            public string CategoriesXmlFilePath = "resources/categories.xml";
            public string FeaturesXmlFilePath = "resources/features.xml";
            public string PerceptionsXmlFilePath = "resources/perceptions.xml";
        }

        private string _settingsFilePath = "resources/modelSettings.xml";

        public TrustModelSettings modelSettings;

        public CategoriesManager categoriesManager;
        public FeaturesManager featuresManager;
        public PerceptionsManager perceptionsManager;


        public TrustModelManager()
        {
            modelSettings = new TrustModelSettings();
            modelSettings.Save(_settingsFilePath);
        }

        public TrustModelManager(string settingsFilePath)
        {
            _settingsFilePath = settingsFilePath;
            modelSettings = TrustModelSettings.LoadOrCreate(_settingsFilePath);
        }


        private void LoadManagers()
        {
            categoriesManager = new CategoriesManager(modelSettings.CategoriesXmlFilePath);
            featuresManager = new FeaturesManager(modelSettings.FeaturesXmlFilePath);
            perceptionsManager = new PerceptionsManager(modelSettings.PerceptionsXmlFilePath);
        }
        

    }
}
