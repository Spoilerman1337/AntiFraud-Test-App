using AntiFraudUI.Models;
using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AntiFraudUI.ViewModels
{
    public class CheckBinViewModel : Screen
    {
        private readonly BindableCollection<RuleModel> _ruleModels;

        private string _withdrawStatus = string.Empty;
        private string _depositStatus = string.Empty;
        private string _bin;
        private JArray jArray;
        BinInfoModel[] binInfoArray = new BinInfoModel[] { };

        public CheckBinViewModel(BindableCollection<RuleModel> ruleModels)
        {
            _ruleModels = ruleModels;

            //Инициализируем json-файл с данными по картам
            //Файл должен находиться в папке с программой
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (StreamReader reader = File.OpenText(dir + "/data.json"))
            {
                jArray = (JArray)JToken.ReadFrom(new JsonTextReader(reader));
            }

            binInfoArray = DeserializeJson(jArray);
        }

        public string WithdrawStatus
        {
            get { return _withdrawStatus; }
            set { _withdrawStatus = value; }
        }

        public string DepositStatus
        {
            get { return _depositStatus; }
            set { _depositStatus = value; }
        }

        public string Bin
        {
            get { return _bin; }
            set
            {
                _bin = value;
                NotifyOfPropertyChange(() => Bin);
            }
        }

        public void CheckBinButton(string bin)
        {
            _withdrawStatus = string.Empty;
            _depositStatus = string.Empty;
            var result = binInfoArray.ToList().FirstOrDefault(i => i.Bin == bin);
            if (result != null)
            {
                CheckBin(result);
            }
            else
            {
                MessageBox.Show("BIN не найден.");
            }
        }

        private BinInfoModel[] DeserializeJson(JArray jarr)
        {
            var BinInfoList = new BinInfoModel[] { };

            try
            {
                BinInfoList = JsonConvert.DeserializeObject<BinInfoModel[]>(jarr.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BinInfoList;
        }

        private void CheckBin(BinInfoModel result)
        {
            foreach (var item in _ruleModels)
            {
                string response = string.Empty;

                //Если элемент правила совпадает с элементом данных карты, то генерируется сообщение
                if (item.Bin != null && item.Bin == result.Bin)
                    response += $"\nBIN == {item.Bin}";
                if (item.Brand != null && item.Brand == result.Brand)
                    response += $"\nBRAND == {item.Brand}";
                if (item.BinType != null && item.BinType == result.Bin_Type)
                    response += $"\nBIN_TYPE == {item.BinType}";
                if (item.BinLevel != null && item.BinLevel == result.Bin_Level)
                    response += $"\nBIN_LEVEL == {item.BinLevel}";
                if (item.Country2Iso != null && item.Country2Iso == result.Country2_Iso)
                    response += $"\nCOUNTRY2_ISO == {item.Country2Iso}";

                //Если хотя бы одно правило было затронуто, то эта информация передаётся в UI
                if (response != string.Empty)
                {
                    switch (item.TransactionType)
                    {
                        case "Deposit":
                            _depositStatus += "Пополнение запрещено:" + response;
                            NotifyOfPropertyChange(() => DepositStatus);
                            break;
                        case "Withdraw":
                            _withdrawStatus += "Списание запрещено:" + response;
                            NotifyOfPropertyChange(() => WithdrawStatus);
                            break;
                        default:
                            throw new ArgumentException("Unexpected transaction type");
                    }
                }
            }
            //В противном случае мы сообщаем UI что правил затронуто не было и транзакция разрешена
            if (_depositStatus == string.Empty)
            {
                _depositStatus += "Пополнение разрешено";
                NotifyOfPropertyChange(() => DepositStatus);
            }
            if (_withdrawStatus == string.Empty)
            {
                _withdrawStatus += "Списание разрешено";
                NotifyOfPropertyChange(() => WithdrawStatus);
            }
        }
    }
}
