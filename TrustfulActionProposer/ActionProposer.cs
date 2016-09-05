using RapportActionProposer.RCPluginDefinition;
using RapportAgentPlugin;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using TrustModel;
using TrustModel.Perceptions;
using TrustModel.Actions;
using TrustPlugin.UI;
using System;
using TrustPlugin.ViewModels;
using HelpersForNet;
using TrustPlugin;

namespace TrustfulActionProposer
{

    public class ActionProposerSettings
    {
        public string Character { get; set; } = "QuickNumbers";
        public string ModelSettingsPath { get; set; } = "Resources/modelSettings.xml";

        public Dictionary<PerceptionModel, Dictionary<string, bool>> PerceptionMapping;
    }

    [Export(typeof(IRCPlugin))]
    public class ActionProposer : EffectorPlugin<ActionProposerSettings>
    {
        public AgentActionsManager AgentActionsManager { get; private set; }
        public override Type WindowType => typeof(UI.MainWindow);

        private TrustModelView Model = Singleton<TrustModelView>.Instance;

        private QuickNumberUnityThalamusClient client;
        public TrustModelManager model;
        public PerceptionMapperManager perceptionMapper;


        public ActionProposer() : base("ActionProposer")
        {
        }

        public void InitModel()
        {
            model = new TrustModelManager(OptionFolderPath + "/");
            perceptionMapper = new PerceptionMapperManager(OptionFolderPath + "/");
            Model.TrustModel = model;
            Model.PerceptionMapper = perceptionMapper;

        }

        public override void InitDependencies()
        {
            base.InitDependencies();
            AgentActionsManager = RapportController.GetPlugin<AgentActionsManager>();
            client = new QuickNumberUnityThalamusClient(this, AgentActionsManager, Name, Settings.Character);
            InitModel();
        }

        public override void Dispose()
        {
            base.Dispose();
            client.Dispose();
        }


    }
}