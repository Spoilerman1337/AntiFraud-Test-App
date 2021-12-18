using Caliburn.Micro;

namespace AntiFraudUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly CheckBinViewModel _checkBinViewModel;
        private readonly SetRulesViewModel _setRulesViewModel;

        public ShellViewModel(CheckBinViewModel checkBinViewModel, SetRulesViewModel setRulesViewModel)
        {
            _checkBinViewModel = checkBinViewModel;
            _setRulesViewModel = setRulesViewModel;
        }

        public async void LoadCheckBin()
        {
            await ActivateItemAsync(_checkBinViewModel);
        }

        public async void LoadSetRules()
        {
            await ActivateItemAsync(_setRulesViewModel);
        }
    }
}
