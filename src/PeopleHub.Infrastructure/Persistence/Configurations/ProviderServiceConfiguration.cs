using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ProviderServiceConfiguration : IEntityTypeConfiguration<ProviderService>
{
    public void Configure(EntityTypeBuilder<ProviderService> builder)
    {
        builder.ToTable("ProviderServices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.BasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.EstimatedDurationMinutes)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.ProviderProfile)
            .WithMany()
            .HasForeignKey(x => x.ProviderProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ServiceCategory)
            .WithMany()
            .HasForeignKey(x => x.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ProviderProfileId);

        builder.HasIndex(x => x.ServiceCategoryId);

        builder.HasIndex(x => new
        {
            x.ProviderProfileId,
            x.ServiceCategoryId
        });
    }
}