using BazaarNotifier.Lib.APIResponses;
using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
            throw new NotImplementedException();
        }
    }
}
