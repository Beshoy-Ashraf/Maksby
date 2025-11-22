using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.Data.Config;

public class MterialConfig : IEntityTypeConfiguration<Material>
{
      public void Configure(EntityTypeBuilder<Material> builder)
      {
            builder.ToTable("Material");

      }
}

