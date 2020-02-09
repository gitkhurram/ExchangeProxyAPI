using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.Common.POCO
{
    public class ECBHistoryInput
    {
        public DateTime[] dates { get; set; }
        public string from_currency { get; set; }
        public string to_currency { get; set; }
    }
}
