
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Configurations;

public class AccountItemConfiguration : IEntityTypeConfiguration<AccountItem>
{
    public void Configure(EntityTypeBuilder<AccountItem> builder)
    {

        //builder.Property(a => a.AccountId)
        //    .IsRequired();

        builder
            .HasMany<ReadingItem>(ac => ac.ReadingItems)
            .WithOne(r => r.Account)
            .IsRequired()
            // Cascade on Delete 
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
