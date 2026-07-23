using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Notifications;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class NotificationConfiguration
    : IEntityTypeConfiguration<Notification>
{
    public void Configure(
        EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");


        builder.HasKey(x => x.Id);


        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);


        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(1000);


        builder.Property(x => x.Type)
            .IsRequired();


        builder.Property(x => x.UserId)
            .IsRequired();


        builder.Property(x => x.IsRead)
            .IsRequired();


        builder.HasIndex(x => x.UserId);
    }
}