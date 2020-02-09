using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace ExchangeAPI.ExchangeServices
{
    public class ECBExchangeServiceProxy : IExchangeServiceProxy
    {
        public string ServiceURL { get; }
        HttpClient client;

        public ECBExchangeServiceProxy(string ServiceURL)
        {
            this.ServiceURL = ServiceURL;
            this.client = new HttpClient();
        }

        public Task<HttpResponseMessage> getExchangeRate(DateTime Date, string FromCurrency, string ToCurrency)
        {
            UriBuilder uriBuilder = new UriBuilder(ServiceURL);
            string strDateRangeQuery = String.Format("start_at={0}&end_at={1}", Date.ToString("yyyy-MM-dd"), Date.ToString("yyyy-MM-dd"));
            uriBuilder.Query = uriBuilder.Query != null && uriBuilder.Query.Length > 0 ? 
                                String.Join(uriBuilder.Query, "&",  strDateRangeQuery) :
                                String.Join("?", strDateRangeQuery);
            uriBuilder.Query = String.Format("{0}&base={1}", uriBuilder.Query, FromCurrency);
            uriBuilder.Query = String.Format("{0}&symbols={1}", uriBuilder.Query, ToCurrency);
            return client.GetAsync(uriBuilder.Uri);
        }

        
    }
}
