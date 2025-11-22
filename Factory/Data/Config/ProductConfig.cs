using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.Data.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
      public void Configure(EntityTypeBuilder<Product> builder)
      {
            builder.ToTable("Product");

      }
}
