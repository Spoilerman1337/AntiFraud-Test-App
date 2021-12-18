using AntiFraudUI.Models;
using Caliburn.Micro;

namespace AntiFraudUI.ViewModels
{
    public class SetRulesViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private readonly BindableCollection<RuleModel> _ruleModels;

        public SetRulesViewModel(IWindowManager windowManager, BindableCollection<RuleModel> ruleModels)
        {
            _windowManager = windowManager;
            _ruleModels = ruleModels;
        }

        public BindableCollection<RuleModel> Rules
        {
            get { return _ruleModels; }
        }

        public void SetRuleButton()
        {
            RuleModel ruleModel = new RuleModel();
            _windowManager.ShowDialogAsync(new UpdateRulesViewModel(ruleModel));
            _ruleModels.Add(ruleModel);
        }

        public void UpdateRuleButton(RuleModel ruleModel)
        {
            _windowManager.ShowDialogAsync(new UpdateRulesViewModel(ruleModel));
            _ruleModels.Refresh();
        }

        public void DeleteRuleButton(RuleModel ruleModel)
        {
            _ruleModels.Remove(ruleModel);
        }
    }
}
