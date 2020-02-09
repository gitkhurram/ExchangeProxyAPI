using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExchangeAPI.ExchangeServices
{
    public class ExchangeServiceClient : IExchangeServiceClient
    {
        public string ServiceURL { get { return this.serviceProxy.ServiceURL; } }
        IExchangeServiceProxy serviceProxy;

        public ExchangeServiceClient(IExchangeServiceProxy proxy)
        {
            this.serviceProxy = proxy;
        }

        private async Task<string> getExchangeRate(DateTime dt, string FromCurrency, string ToCurrency)
        {
            HttpResponseMessage ResponseMessage;
            ResponseMessage = await this.serviceProxy.getExchangeRate(dt, FromCurrency, ToCurrency);            
            return await ResponseMessage.Content.ReadAsStringAsync();
        }

        protected virtual async Task<IDictionary<DateTime, double>> getExcchangeRates(DateTime[] dates, string FromCurrency, string ToCurrency)
        {   
            Dictionary<DateTime, double> dicRates = new Dictionary<DateTime, double>();

            IEnumerable<Task<string>> getExchangeRateQuery = from dt in dates select getExchangeRate(dt, FromCurrency, ToCurrency);
            Task<string>[] getExchangeRateTasks = getExchangeRateQuery.ToArray();
            string[] responses = await Task.WhenAll(getExchangeRateTasks);
            
            foreach (string response in responses)
            {
                try
                {
                    JObject data = JObject.Parse(response);
                    Dictionary<DateTime, double> rate = data.Value<JObject>("rates").Properties().ToDictionary(x => DateTime.Parse(x.Name), y => y.Value.Value<double>(ToCurrency));
                    rate.ToList().ForEach(p => dicRates[p.Key] = p.Value);
                }
                catch
                {}
            }

            return dicRates;
        }

        public async Task<Tuple<KeyValuePair<DateTime, double>, KeyValuePair<DateTime, double>, double>> getExchangeRateInfo(DateTime[] dates, string FromCurrency, string ToCurrency)
        {
            IDictionary<DateTime, double> dicRates = new Dictionary<DateTime, double>();            
            dicRates = await this.getExcchangeRates(dates, FromCurrency, ToCurrency);
            KeyValuePair<DateTime, double> Min, Max;
            double Avg; 
            if (dicRates.Count > 0)
            { 
                Avg = dicRates.Sum(x => x.Value) / dicRates.Count;
                Min = dicRates.FirstOrDefault(x => x.Value == dicRates.Values.Min());
                Max = dicRates.FirstOrDefault(x => x.Value == dicRates.Values.Max());
                return Tuple.Create< KeyValuePair<DateTime, double>, KeyValuePair<DateTime, double>, double>(Min, Max, Avg);
            }


            return null;
        }        
    }
}
