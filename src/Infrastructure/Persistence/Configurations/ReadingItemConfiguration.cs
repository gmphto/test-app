using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ReadingItemConfiguration : IEntityTypeConfiguration<MeterReadItem>
{
    public void Configure(EntityTypeBuilder<MeterReadItem> builder)
    {
        builder.Property(t => t.Id)
            .IsRequired();


        builder.Property(t => t.Value)
            .IsRequired();

        builder.Property(t => t.Date)
            .IsRequired();

    }
}
