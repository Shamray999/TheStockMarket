using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TheStockMarket.Services
{
    public interface IStockMarketChannel
    {
        
    }

    public class StockMarketChannel
    {
        public Channel<KeyValuePair<string, decimal>> Channel;

        public StockMarketChannel()
        {
            Channel = System.Threading.Channels.Channel.CreateUnbounded<KeyValuePair<string, decimal>>();
        }
    }
}
