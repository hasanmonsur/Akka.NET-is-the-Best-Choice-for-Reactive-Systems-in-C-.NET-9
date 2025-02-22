using AgriMarketAnalysis.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriMarketAnalysis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AgriculturalGood> AgriculturalGoods { get; set; }
    }
}
