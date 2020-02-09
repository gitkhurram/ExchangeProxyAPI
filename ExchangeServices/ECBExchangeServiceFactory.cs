using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ExchangeAPI.ExchangeServices
{
    public class ECBExchangeServiceFactory : IExchangeFactory
    {   
        public IExchangeServiceProxy createExchangeServiceProxy(string ECBExchangeServiceURL)
        {
            return new ECBExchangeServiceProxy(ECBExchangeServiceURL);
        }
    }
}
