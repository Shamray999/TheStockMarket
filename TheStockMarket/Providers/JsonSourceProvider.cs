using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TheStockMarket.Models;
using TheStockMarket.Services;

namespace TheStockMarket.Providers
{
    public class JsonSourceProvider : ISourceProvider
    {
        private readonly string _filePath = @"Data/stocks.json";
        private readonly StockMarketChannel _stockMarketChannel;

        public JsonSourceProvider(StockMarketChannel stockMarketChannel)
        {
            _stockMarketChannel = stockMarketChannel;
        }

        public async Task GetStocks()
        {
            try
            {
                using FileStream openStream = File.OpenRead(_filePath);
                List<StockModelJson> json = await JsonSerializer.DeserializeAsync<List<StockModelJson>>(openStream);

                foreach (var item in json)
                {
                    var stock = new KeyValuePair<string, decimal>(item.name, item.price);
                    await _stockMarketChannel.Channel.Writer.WriteAsync(stock);
                }
            }
            catch (Exception ex)
            {
                // Log
            }
        }
    }
}
