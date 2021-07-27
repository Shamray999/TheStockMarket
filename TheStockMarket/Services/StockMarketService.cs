using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStockMarket.Models;

namespace TheStockMarket.Services
{
    public class StockMarketService
    {
        private ConcurrentDictionary<string, decimal> Stocks { get; set; } = new ConcurrentDictionary<string, decimal>();

        public StockModel GetLowestPrice(string stockName)
        {
            if (Stocks.TryGetValue(stockName, out decimal price))
                return new StockModel() { Name = stockName, Price = price };

            return null;
        }

        public List<StockModel> GetAllLowestPrices()
        {
            return Stocks.Select(s => new StockModel() { Name = s.Key, Price = s.Value }).ToList();
        }

        public void UpdateStockToMin(KeyValuePair<string, decimal> stock)
        {
            if (Stocks.TryAdd(stock.Key, stock.Value) == false)
            {
                if (Stocks[stock.Key] > stock.Value)
                {
                    Stocks[stock.Key] = stock.Value;
                }
            }
        }
    }
}
