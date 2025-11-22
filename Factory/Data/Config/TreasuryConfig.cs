using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.Data.Config;

public class TreasuryConfig : IEntityTypeConfiguration<Treasury>
{
      public void Configure(EntityTypeBuilder<Treasury> builder)
      {
            builder.ToTable("Treasury");

      }
}

