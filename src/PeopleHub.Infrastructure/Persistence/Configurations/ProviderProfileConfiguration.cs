using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ProviderProfileConfiguration : IEntityTypeConfiguration<ProviderProfile>
{
    public void Configure(EntityTypeBuilder<ProviderProfile> builder)
    {
        builder.ToTable("ProviderProfiles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Bio)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.ExperienceYears)
            .IsRequired();

        builder.Property(x => x.VerificationStatus)
            .IsRequired();

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        builder.HasMany(x => x.Skills)
            .WithOne()
            .HasForeignKey(x => x.ProviderProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}