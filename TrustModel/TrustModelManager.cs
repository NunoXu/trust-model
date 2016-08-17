using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;
using TrustModel.Perceptions;
using TrustModel.Util;

namespace TrustModel
{
    public class TrustModelManager
    {
        public class TrustModelSettings : XmlHolder<TrustModelSettings>
        {
            public string CategoriesXmlFilePath = "TrustModel/categories.xml";
            public string FeaturesXmlFilePath = "TrustModel/features.xml";
            public string PerceptionsXmlFilePath = "TrustModel/perceptions.xml";
            public string AgentsXmlFilePath = "TrustModel/agents.xml";

            public static TrustModelSettings LoadOrCreate(string filePath, string defaultFolderPath)
            {
                if (Exists(filePath))
                {
                    return Load(filePath);
                }
                else
                {
                    var instance = new TrustModelSettings();
                    instance.CategoriesXmlFilePath = defaultFolderPath + instance.CategoriesXmlFilePath;
                    instance.FeaturesXmlFilePath = defaultFolderPath + instance.FeaturesXmlFilePath;
                    instance.PerceptionsXmlFilePath = defaultFolderPath + instance.PerceptionsXmlFilePath;
                    instance.AgentsXmlFilePath = defaultFolderPath + instance.AgentsXmlFilePath;
                    instance.Save(filePath);
                    return instance;
                }
            }
        }
        
        private string _settingsFilePath = "TrustModel/modelSettings.xml";
        public string SettingsFilePath {
            get { return _settingsFilePath; }
            set {
                _settingsFilePath = value;
                ModelSettings = TrustModelSettings.LoadOrCreate(_settingsFilePath);
            }
        }

        public TrustModelSettings ModelSettings;



        public TrustModelManager()
        {
            ModelSettings = TrustModelSettings.LoadOrCreate(SettingsFilePath);
            LoadManagers();
        }

        public TrustModelManager(string modelFolderPath)
        {
            _settingsFilePath = modelFolderPath + _settingsFilePath;
            
            ModelSettings = TrustModelSettings.LoadOrCreate(SettingsFilePath, modelFolderPath);
            LoadManagers();
        }


        private void LoadManagers()
        {
            CategoriesManager.Instance.FilePath = ModelSettings.CategoriesXmlFilePath;
            FeaturesManager.Instance.FilePath = ModelSettings.FeaturesXmlFilePath;
            PerceptionsManager.Instance.FilePath = ModelSettings.PerceptionsXmlFilePath;
            AgentsManager.Instance.FilePath = ModelSettings.AgentsXmlFilePath;
        }
    }
}
