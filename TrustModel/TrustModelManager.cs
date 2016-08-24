using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrustModel.Features;
using TrustModel.Perceptions;
using TrustModel.Util;

namespace TrustModel
{
    public class TrustModelManager : INotifyPropertyChanged
    {
        public class TrustModelSettings : XmlHolder<TrustModelSettings>
        {
            private string _categoriesXmlFilePath = "TrustModel/categories.xml";
            public string CategoriesXmlFilePath {
                get { return _categoriesXmlFilePath; }
                set {
                    _categoriesXmlFilePath = value;
                    NotifyPropertyChanged();
                    CategoriesManager.Instance.FilePath = _categoriesXmlFilePath;
                    Save();
                }
            }

            private string _featuresXmlFilePath = "TrustModel/features.xml";
            public string FeaturesXmlFilePath {
                get { return _featuresXmlFilePath; }
                set {
                    _featuresXmlFilePath = value;
                    NotifyPropertyChanged();
                    FeaturesManager.Instance.FilePath = _featuresXmlFilePath;
                    Save();
                } }

            private string _perceptionsXmlFilePath = "TrustModel/perceptions.xml";
            public string PerceptionsXmlFilePath {
                get { return _perceptionsXmlFilePath; }
                set {
                    _perceptionsXmlFilePath = value;
                    NotifyPropertyChanged();
                    PerceptionsManager.Instance.FilePath = _perceptionsXmlFilePath;
                    Save();
                } }

            private string _agentsXmlFilePath = "TrustModel/agents.xml";
            public string AgentsXmlFilePath {
                get { return _agentsXmlFilePath; }
                set {
                    _agentsXmlFilePath = value;
                    NotifyPropertyChanged();
                    AgentsManager.Instance.FilePath = _agentsXmlFilePath;
                    Save();
                } }
            

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

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _settingsFilePath = "TrustModel/modelSettings.xml";
        public string SettingsFilePath {
            get { return _settingsFilePath; }
            set {
                _settingsFilePath = value;
                ModelSettings = TrustModelSettings.LoadOrCreate(_settingsFilePath);
                NotifyPropertyChanged();
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
