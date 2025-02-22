using AgriMarketAnalysis.Data;
using AgriMarketAnalysis.Models;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;

namespace AgriMarketAnalysis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketAnalysisController : ControllerBase
    {
        private readonly IActorRef _marketDataProcessorActor;
        private readonly AppDbContext _dbContext;

        public MarketAnalysisController(IActorRef marketDataProcessorActor, AppDbContext dbContext)
        {
            _marketDataProcessorActor = marketDataProcessorActor;
            _dbContext = dbContext;
        }

        // Endpoint to add new market data
        [HttpPost("add")]
        public IActionResult AddMarketData([FromBody] AgriculturalGood good)
        {
            _marketDataProcessorActor.Tell(good);
            return Ok();
        }

        // Endpoint to get all market data
        [HttpGet("all")]
        public IActionResult GetAllMarketData()
        {
            var data = _dbContext.AgriculturalGoods.ToList();
            return Ok(data);
        }
    }
}
