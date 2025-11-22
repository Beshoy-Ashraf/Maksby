using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.Data.Config;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
      public void Configure(EntityTypeBuilder<Seller> builder)
      {
            builder.ToTable("Seller");

      }
}
