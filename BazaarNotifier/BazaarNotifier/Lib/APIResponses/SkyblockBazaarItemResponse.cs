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
    }
}
