namespace AntiFraudUI.Models
{
    //Модель является представлением данных предоставленных BIN
    public class BinInfoModel
    {
        public string Id { get; set; }
        public string Bin { get; set; }
        public string Brand { get; set; }
        public string Bank_Name { get; set; }
        public string Bin_Type { get; set; }
        public string Bin_Level { get; set; }
        public string Country_Iso { get; set; }
        public string Country2_Iso { get; set; }
        public string Country3_Iso { get; set; }
    }
}
