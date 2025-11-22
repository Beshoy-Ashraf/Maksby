using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.Data.Config;

public class TraderConfiguration : IEntityTypeConfiguration<Trader>
{
      public void Configure(EntityTypeBuilder<Trader> builder)
      {
            builder.ToTable("Trader");


      }
}
