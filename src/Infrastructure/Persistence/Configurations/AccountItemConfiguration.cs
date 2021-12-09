
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AccountItemConfiguration : IEntityTypeConfiguration<AccountItem>
{
    public void Configure(EntityTypeBuilder<AccountItem> builder)
    {

        builder.Property(a => a.Id)
            .IsRequired();

        builder
            .HasMany<MeterReadItem>(ac => ac.ReadingItems)
            .WithOne(r => r.Account)
            .IsRequired()
            // Cascade on Delete 
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
