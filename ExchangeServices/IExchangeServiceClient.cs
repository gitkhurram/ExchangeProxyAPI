using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.ExchangeServices
{
    public interface IExchangeServiceClient
    {
        public string ServiceURL { get; }
        Task<Tuple<KeyValuePair<DateTime, double>, KeyValuePair<DateTime, double>, double>> getExchangeRateInfo(DateTime[] dates, string FromCurrency, string ToCurrency);        
    }
}
