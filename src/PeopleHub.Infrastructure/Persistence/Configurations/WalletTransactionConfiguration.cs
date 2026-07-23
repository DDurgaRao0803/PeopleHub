using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class WalletTransactionConfiguration
    : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(
        EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.ToTable("WalletTransactions");


        builder.HasKey(x => x.Id);



        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);



        builder.Property(x => x.Type)
            .HasConversion<int>();



        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();



        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();



        builder.HasOne<Wallet>()
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}