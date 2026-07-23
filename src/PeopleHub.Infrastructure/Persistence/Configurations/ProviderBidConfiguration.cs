using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Bidding;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ProviderBidConfiguration
    : IEntityTypeConfiguration<ProviderBid>
{
    public void Configure(
        EntityTypeBuilder<ProviderBid> builder)
    {
        builder.ToTable("ProviderBids");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);


        builder.Property(x => x.Status)
            .HasConversion<int>();


        builder.Property(x => x.EstimatedMinutes)
            .IsRequired();


        builder.HasIndex(x =>
            new
            {
                x.ServiceRequestId,
                x.ProviderProfileId
            })
            .IsUnique(false);
    }
}