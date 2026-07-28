using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Review;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.ServiceRequestId)
            .IsUnique();

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ProviderProfile)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.ProviderProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ServiceRequest)
            .WithOne(x => x.Review)
            .HasForeignKey<Review>(x => x.ServiceRequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}