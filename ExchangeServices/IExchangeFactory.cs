using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeAPI.ExchangeServices
{
    public interface IExchangeFactory
    {
        IExchangeServiceProxy createExchangeServiceProxy(string ECBExchangeServiceURL);
    }
}
