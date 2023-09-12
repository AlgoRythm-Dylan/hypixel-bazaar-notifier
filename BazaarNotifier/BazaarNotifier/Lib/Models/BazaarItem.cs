using BazaarNotifier.Lib.APIResponses;
using System.Collections.Generic;
using System.Linq;
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
        public List<SkyblockBazaarItemOrderSummary> SellOrderList { get; set; }
        public List<SkyblockBazaarItemOrderSummary> BuyOrderList { get; set; }
        public double TopSellOrderPrice
        {
            get
            {
                var topSellOrder = SellOrderList?.FirstOrDefault();
                if(topSellOrder != null)
                {
                    return topSellOrder.PricePerUnit;
                }
                return 0;
            }
        }
        public double TopBuyOrderPrice
        {
            get
            {
                var topBuyOrder = BuyOrderList?.FirstOrDefault();
                if (topBuyOrder != null)
                {
                    return topBuyOrder.PricePerUnit;
                }
                return 0;
            }
        }
    }
}
