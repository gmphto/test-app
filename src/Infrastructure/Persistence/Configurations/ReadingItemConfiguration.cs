using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ReadingItemConfiguration : IEntityTypeConfiguration<ReadingItem>
{
    public void Configure(EntityTypeBuilder<ReadingItem> builder)
    {
        builder.Property(t => t.ReadingId)
            .IsRequired();

        builder.Property(t => t.Value)
            .IsRequired();

        builder.Property(t => t.Date)
            .IsRequired();

        // FK Configuration
        //builder
        //    .HasOne<AccountItem>(ac => ac.Account)
        //    .WithMany(g => g.ReadingItems)
        //    .HasForeignKey(ac => ac.AccountId);
    }
}
