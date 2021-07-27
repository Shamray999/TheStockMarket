using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheStockMarket.Services;

namespace TheStockMarket.Providers
{
    public class CsvSourceProvider : ISourceProvider
    {
        private readonly string _filePath = @"Data/stocks.csv";
        private readonly StockMarketChannel _stockMarketChannel;

        public CsvSourceProvider(StockMarketChannel stockMarketChannel)
        {
            _stockMarketChannel = stockMarketChannel;
        }

        public async Task GetStocks()
        {
            using var reader = new StreamReader(_filePath);

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                try
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    var stock = new KeyValuePair<string, decimal>(values[0], decimal.Parse(values[2]));

                    await _stockMarketChannel.Channel.Writer.WriteAsync(stock);
                }
                catch (Exception)
                {
                    // TODO: Add Log
                }
            }
        }
    }
}
