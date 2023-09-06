using System.Text.Json.Serialization;

namespace BazaarNotifier.Lib.Models
{
    public class BazaarItem
    {
        [JsonPropertyName("productId")]
        public string ID { get; set; }
        public string Name { get; set; }
        public double SellPrice { get; set; }
        public long SellVolume { get; set; }
        public long SellMovingWeek { get; set; }
        public int SellOrders { get; set; }
        public double BuyPrice { get; set; }
        public long BuyVolume { get; set; }
        public long BuyMovingWeek { get; set; }
        public int BuyOrders { get; set; }
    }
}
