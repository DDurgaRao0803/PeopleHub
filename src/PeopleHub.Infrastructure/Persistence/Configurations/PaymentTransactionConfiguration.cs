using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class PaymentTransactionConfiguration
    : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(
        EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("PaymentTransactions");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);


        builder.Property(x => x.Status)
            .HasConversion<int>();


        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();
    }
}