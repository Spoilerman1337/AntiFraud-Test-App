using AntiFraudUI.Models;
using Caliburn.Micro;
using System.Collections.Generic;

namespace AntiFraudUI.ViewModels
{
    public class UpdateRulesViewModel : Screen
    {
        private RuleModel _rule;
        private readonly BindableCollection<string> _transactionTypes = new() { "Withdraw", "Deposit" };
        private string _selecetedTransactionType;
        public UpdateRulesViewModel(RuleModel ruleModel)
        {
            _rule = ruleModel;
        }

        public string RuleName
        {
            get { return _rule.Name; }
            set { _rule.Name = value; }
        }

        public string RuleBin
        {
            get { return _rule.Bin; }
            set { _rule.Bin = value; }
        }

        public string RuleBrand
        {
            get { return _rule.Brand; }
            set { _rule.Brand = value; }
        }

        public string RuleBinLevel
        {
            get { return _rule.BinLevel; }
            set { _rule.BinLevel = value; }
        }

        public string RuleBinType
        {
            get { return _rule.BinType; }
            set { _rule.BinType = value; }
        }

        public string RuleCountry2Iso
        {
            get { return _rule.Country2Iso; }
            set { _rule.Country2Iso = value; }
        }

        public string RuleTransactionType
        {
            get { return _rule.TransactionType; }
            set { _rule.TransactionType = value; }
        }

        public BindableCollection<string> TransactionTypes
        {
            get { return _transactionTypes; }
        }

        public string SelectedTransactionType
        {
            get { return _selecetedTransactionType; }
            set
            {
                _selecetedTransactionType = RuleTransactionType;
                RuleTransactionType = value;
                NotifyOfPropertyChange(() => SelectedTransactionType);
                NotifyOfPropertyChange(() => RuleTransactionType);
            }
        }
    }
}
