using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ProviderVerificationConfiguration : IEntityTypeConfiguration<ProviderVerification>
{
    public void Configure(EntityTypeBuilder<ProviderVerification> builder)
    {
        builder.ToTable("ProviderVerifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProviderProfileId)
            .IsRequired();

        builder.Property(x => x.GovernmentIdType)
            .IsRequired();

        builder.Property(x => x.GovernmentIdNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FrontImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.BackImageUrl)
            .HasMaxLength(500);

        builder.Property(x => x.SelfieImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.VerificationStatus)
            .IsRequired();

        builder.Property(x => x.RejectionReason)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.ProviderProfileId)
            .IsUnique();
    }
}