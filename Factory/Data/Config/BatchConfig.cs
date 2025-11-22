using Factory.Data.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factory.Data.Config;

public class BatchConfig : IEntityTypeConfiguration<batch>
{
      public void Configure(EntityTypeBuilder<batch> builder)
      {
            builder.ToTable("Batch");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                  .WithMany()
                  .HasForeignKey("ProductId");

            builder.HasMany(x => x.Materials)
                  .WithMany()
                  .UsingEntity(j => j.ToTable("BatchMaterials"));

      }
}
