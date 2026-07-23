using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class WalletConfiguration
    : IEntityTypeConfiguration<Wallet>
{
    public void Configure(
        EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.Balance)
            .HasPrecision(18, 2);


        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();


        builder.HasIndex(x => x.ProviderProfileId)
            .IsUnique();
    }
}