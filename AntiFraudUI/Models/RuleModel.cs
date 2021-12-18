namespace AntiFraudUI.Models
{
    //Модель является представлением правил, по которым идёт валидация BIN-кода
    public class RuleModel
    {
        public string Name { get; set; }
        public string Bin { get; set; }
        public string Brand { get; set; }
        public string BinType { get; set; }
        public string BinLevel { get; set; }
        public string Country2Iso { get; set; }
        public string TransactionType { get; set; }
    }
}
