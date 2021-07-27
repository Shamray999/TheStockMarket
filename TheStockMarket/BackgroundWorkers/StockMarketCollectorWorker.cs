using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheStockMarket.Services;

namespace TheStockMarket.BackgroundWorkers
{
    public class StockMarketCollectorWorker : BackgroundService
    {
        private readonly StockMarketCollector _stockMarketCollector;
        private readonly int _refreshWorker = 20000;

        public StockMarketCollectorWorker(StockMarketCollector stockMarketCollector)
        {
            _stockMarketCollector = stockMarketCollector;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                await _stockMarketCollector.CollectStocks();

                await Task.Delay(_refreshWorker, stoppingToken);
            }
        }
    }
}
