using BazaarNotifier.Lib.APIResponses;
using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib
{
    public class HypixelAPI
    {
        private HttpClient HttpClient { get; set; }
        public HypixelAPI(string APIKey = null)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://api.hypixel.net/");
            if (!string.IsNullOrEmpty(APIKey))
            {
                HttpClient.DefaultRequestHeaders.Add("API-Key", APIKey);
            }
        }

        public async Task<List<Item>> GetSkyblockItems()
        {
            return (await HttpClient.GetFromJsonAsync<SkyblockItemsResponse>("resources/skyblock/items")).Items;
        }

        public async Task<List<BazaarItem>> GetBazaar()
        {
            var response = await HttpClient.GetFromJsonAsync<SkyblockBazaarResponse>("skyblock/bazaar");
            var list = new List<BazaarItem>();
            foreach(var item in response.Items)
            {
                var bzItem = item.Value.BazaarItem;
                // These items are flipped because uhhh buy summary refers
                // to insta-buyers / sellers. Buy summary is the list of
                // sell orders. Terrible nomenclature but it is what it is.
                bzItem.SellOrderList = item.Value.BuySummary;
                bzItem.BuyOrderList = item.Value.SellSummary;
                list.Add(bzItem);
            }
            return list;
        }
    }
}
