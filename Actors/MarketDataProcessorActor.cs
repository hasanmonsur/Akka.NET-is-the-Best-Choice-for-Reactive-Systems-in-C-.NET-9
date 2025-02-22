using AgriMarketAnalysis.Data;
using AgriMarketAnalysis.Models;
using Akka.Actor;

namespace AgriMarketAnalysis.Actors
{
    public class MarketDataProcessorActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;

        public MarketDataProcessorActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Receive<AgriculturalGood>(good =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // Save the market data to the database
                    dbContext.AgriculturalGoods.Add(good);
                    dbContext.SaveChanges();

                    Console.WriteLine($"Saved {good.Name} with price {good.Price} to the database.");
                }
            });
        }
    }
}
