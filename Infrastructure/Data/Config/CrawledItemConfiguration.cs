using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class CrawledItemConfiguration : IEntityTypeConfiguration<CrawledItem>
    {
        public void Configure(EntityTypeBuilder<CrawledItem> builder)
        {
            builder.ToTable("Crawled");

            builder.Property(ci => ci.ID)               
                .IsRequired();

            builder.Property(ci => ci.Model)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(ci => ci.Price)
                .IsRequired(false)
                 .HasMaxLength(50);

            builder.Property(ci => ci.Link)
               .IsRequired(false)
              .HasMaxLength(500);

            builder.Property(ci => ci.ImageUrl)
                .IsRequired(false)
               .HasMaxLength(500);

            builder.Property(ci => ci.CrawledTime)
                .IsRequired(true);
        }
    }
}
