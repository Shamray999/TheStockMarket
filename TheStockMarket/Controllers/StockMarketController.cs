using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStockMarket.Models;
using TheStockMarket.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheStockMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMarketController : ControllerBase
    {
        private readonly StockMarketService _stockMarketService;

        public StockMarketController(StockMarketService stockMarketService)
        {
            _stockMarketService = stockMarketService;
        }

        [HttpGet]
        public IEnumerable<StockModel> Get()
        {
            return _stockMarketService.GetAllLowestPrices();
        }

        [HttpGet("{stockName}")]
        public StockModel Get(string stockName)
        {
            return _stockMarketService.GetLowestPrice(stockName);
        }
    }
}
