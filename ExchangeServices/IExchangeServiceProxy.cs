using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeAPI.ExchangeServices
{
    public interface IExchangeServiceProxy
    {
        public string ServiceURL { get; }
        Task<HttpResponseMessage> getExchangeRate(DateTime Date, string FromCurrency, string ToCurrency);
    }
}
