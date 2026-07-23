using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Location;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ProviderLocationConfiguration
    : IEntityTypeConfiguration<ProviderLocation>
{
    public void Configure(
        EntityTypeBuilder<ProviderLocation> builder)
    {
        builder.ToTable("ProviderLocations");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.ProviderProfileId)
            .IsRequired();


        builder.Property(x => x.Latitude)
            .HasPrecision(9, 6)
            .IsRequired();


        builder.Property(x => x.Longitude)
            .HasPrecision(9, 6)
            .IsRequired();


        builder.Property(x => x.UpdatedOnUtc)
            .IsRequired();


        builder.HasIndex(x => x.ProviderProfileId)
            .IsUnique();
    }
}