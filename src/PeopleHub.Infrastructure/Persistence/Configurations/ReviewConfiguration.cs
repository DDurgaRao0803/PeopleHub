using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Review;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ReviewConfiguration
    : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(review => review.Id);

        builder.Property(review => review.CustomerId)
            .IsRequired();

        builder.Property(review => review.ProviderProfileId)
            .IsRequired();

        builder.Property(review => review.ServiceRequestId)
            .IsRequired();

        builder.Property(review => review.Rating)
            .IsRequired();

        builder.Property(review => review.Comment)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(review => review.CreatedOnUtc)
    .IsRequired();

builder.Property(review => review.CreatedBy);

builder.Property(review => review.LastModifiedOnUtc);

builder.Property(review => review.LastModifiedBy);
    }
}