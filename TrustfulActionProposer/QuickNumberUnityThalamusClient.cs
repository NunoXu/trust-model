using HelpersForNet;
using QuickNumberUnityMessages;
using RapportAgentPlugin;
using System;
using Thalamus;
using TrustModel.Perceptions;

namespace TrustfulActionProposer
{
    public class QuickNumberUnityThalamusClient : ThalamusClient, ITUCEventsThalamus
    {
        private ActionProposer plugin;
        private AgentActionsManager actionsManager;
        private ActionProposerSettings Settings => plugin.Settings;

        public QuickNumberUnityThalamusClient(ActionProposer plugin, AgentActionsManager actionsManager, string name, string character) : base(name, character)
        {
            this.plugin = plugin;
            this.actionsManager = actionsManager;
        }


        public void WaitingForMatch() { }
        public void HumanProceededToNextScene() { }

        public void InvestmentAdjustmentStarted(int investment) { }
        public void InvestmentAdjustmentEnded() { }

        public void SessionStarted() { }
        public void TrainingStarted() { }
        public void TrainingMatchStarted() { }
        public void TrainingMatchResults(int totalInvestment, double multiplier, int resourcesEarnerd) { }

        public void MainGameStarted() { }
        public void MatchStarted(int emysInvestment, int humanInvestment) { }
        public void MatchResults(int emysInvestment, int humanInvestment, double emysMultiplier, double humanMultiplier, int emysResourcesEarnerd, int humanResourcesEarnerd) {

          
        }

        public void InvestmentStarted(int initalEmysResources) { }
        public void HumanInvestment(int investment, int humanResources) { }
        public void TrustGameStarted() { }
        public void TrustGameResults(int totalInvestment, double multiplier, int resourcesEarned, int totalEmysResources) { }
        public void InvestmentReturnScreenStarted() { }
        public void InvestmentEnded() { }
        public void EmysSelectingReturnValue(int value) { }
        public void EmysSelectedReturnValue() { }
        public void EmysReturnInvestment(int given, int available) { } //session end

        public void PlayerPressedIncorrectTarget()
        {
        }
        public void PlayerPressedCorrectTarget() {
        }
        public void EmysPressedIncorrectTarget() { }
        public void EmysPressedCorrectTarget() { }
    }
}
