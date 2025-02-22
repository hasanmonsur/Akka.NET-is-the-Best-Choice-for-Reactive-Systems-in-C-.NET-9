namespace AgriMarketAnalysis.Models
{
    public class AgriculturalGood
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., Wheat, Corn, Rice
        public decimal Price { get; set; } // Current market price
        public DateTime Timestamp { get; set; } // Time of data entry
        public string Region { get; set; } // Region of the market
    }
}
