

using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;

namespace Factory.Data.Context;

public class AppDBContext : DbContext
{
      public AppDBContext(DbContextOptions options) : base(options)
      {

      }
      public DbSet<Seller> Sellers { get; set; }
      public DbSet<Trader> Traders { get; set; }

      public DbSet<Expenses> Expenses { get; set; }
      public DbSet<Treasury> Treasury { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Material> Materials { get; set; }
      public DbSet<batch> Batches { get; set; }




      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);
      }
}
