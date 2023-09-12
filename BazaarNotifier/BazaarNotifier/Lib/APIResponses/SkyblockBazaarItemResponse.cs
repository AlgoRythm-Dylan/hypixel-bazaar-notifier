using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib.APIResponses
{
    public class SkyblockBazaarItemResponse
    {
        [JsonPropertyName("quick_status")]
        public BazaarItem BazaarItem { get; set; }
        [JsonPropertyName("buy_summary")]
        public List<SkyblockBazaarItemOrderSummary> BuySummary { get; set; }
        [JsonPropertyName("sell_summary")]
        public List<SkyblockBazaarItemOrderSummary> SellSummary { get; set; }
    }
}
