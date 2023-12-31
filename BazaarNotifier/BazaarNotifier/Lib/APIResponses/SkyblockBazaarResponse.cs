﻿using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib.APIResponses
{
    public class SkyblockBazaarResponse
    {
        [JsonPropertyName("products")]
        public Dictionary<string, SkyblockBazaarItemResponse> Items { get; set; }
    }
}
