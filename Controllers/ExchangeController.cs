using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExchangeAPI.Common.POCO;
using ExchangeAPI.ExchangeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExchangeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {   
        private readonly ILogger<ExchangeController> _logger;
        private readonly IExchangeFactory factory;

        public ExchangeController(ILogger<ExchangeController> logger, IExchangeFactory factory)
        {
            _logger = logger;
            this.factory = factory;
        }

        [HttpPost]
        public async Task<JsonResult> getMaxExchangeRate([FromBody]ECBHistoryInput Input)
        {
            IExchangeServiceClient client = new ExchangeServiceClient(factory.createExchangeServiceProxy("https://api.exchangeratesapi.io/history"));
            Tuple<KeyValuePair<DateTime, double>, KeyValuePair<DateTime, double>, double> result = await client.getExchangeRateInfo(Input.dates, Input.from_currency, Input.to_currency);
            return new JsonResult(String.Format("A minimum rate of {0} on {1}. A max rate of {2} on {3}. An average rate of {4}",
                                        result.Item1.Value, result.Item1.Key.ToString("yyyy-MM-dd"),
                                        result.Item2.Value, result.Item2.Key.ToString("yyyy-MM-dd"),
                                        result.Item3));
        }
    }
}
