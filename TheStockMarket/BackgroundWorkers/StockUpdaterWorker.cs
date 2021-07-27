using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheStockMarket.Services;

namespace TheStockMarket.BackgroundWorkers
{
    public class StockUpdaterWorker : BackgroundService
    {
        private readonly StockMarketChannel _stockMarketChannel;
        private readonly StockMarketService _stockMarketService;

        public StockUpdaterWorker(StockMarketChannel stockMarketChannel,
            StockMarketService stockMarketService)
        {
            _stockMarketChannel = stockMarketChannel;
            _stockMarketService = stockMarketService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var stock in _stockMarketChannel.Channel.Reader.ReadAllAsync(stoppingToken))
            {
                _stockMarketService.UpdateStockToMin(stock);
            }
        }
    }
}
