using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TheStockMarket.Models;
using TheStockMarket.Services;

namespace TheStockMarket.Providers
{
    public class HttpSourceProvider : ISourceProvider
    {
        private readonly string _path = @"https://s3.amazonaws.com/test-data-samples/stocks.json";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly StockMarketChannel _stockMarketChannel;

        public HttpSourceProvider(IHttpClientFactory httpClientFactory, StockMarketChannel stockMarketChannel)
        {
            _httpClientFactory = httpClientFactory;
            _stockMarketChannel = stockMarketChannel;
        }

        public async Task GetStocks()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var res = await httpClient.GetAsync(_path);

            var content = await res.Content.ReadAsStringAsync();

            var jsonItem = JsonConvert.DeserializeObject<List<StockModel>>(content);

            foreach (var item in jsonItem)
            {
                var stock = new KeyValuePair<string, decimal>(item.Name, item.Price);
                await _stockMarketChannel.Channel.Writer.WriteAsync(stock);
            }
        }
    }
}
