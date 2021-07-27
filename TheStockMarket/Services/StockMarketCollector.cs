using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStockMarket.Providers;

namespace TheStockMarket.Services
{
    public class StockMarketCollector
    {
        private readonly IEnumerable<ISourceProvider> _stockMarketProviders;

        public StockMarketCollector(IEnumerable<ISourceProvider> stockMarketProviders)
        {
            _stockMarketProviders = stockMarketProviders;
        }

        public async Task CollectStocks()
        {
            List<Task> providers = new();

            foreach (var item in _stockMarketProviders)
            {
                providers.Add(item.GetStocks());
            }

            await Task.WhenAll(providers);

            //await Task.Run(() => Parallel.ForEach(_stockMarketProviders, provider =>
            //{
            //    provider.GetStocks();
            //}));
        }
    }
}
