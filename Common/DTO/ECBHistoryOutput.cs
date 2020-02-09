using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.Common.POCO
{
    public class ECBHistoryOutput
    {
        
        [JsonProperty("rates")]
        public int rates { get; set; }

        [JsonProperty("start_at")]
        public string start_at { get; set; }

        [JsonProperty("base")]
        public string @base { get; set; }

        [JsonProperty("end_at")]
        public string end_at { get; set; }
    }
}

