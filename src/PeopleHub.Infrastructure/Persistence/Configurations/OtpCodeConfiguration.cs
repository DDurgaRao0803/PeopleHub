using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Otp;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable("OtpCodes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CodeHash)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Purpose)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.Property(x => x.VerifiedAtUtc);

        builder.Property(x => x.FailedAttempts)
            .IsRequired();

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.Purpose
        });
    }
}