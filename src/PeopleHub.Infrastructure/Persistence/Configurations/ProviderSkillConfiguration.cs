
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Persistence.Configurations;

public sealed class ProviderSkillConfiguration : IEntityTypeConfiguration<ProviderSkill>
{
    public void Configure(EntityTypeBuilder<ProviderSkill> builder)
    {
        builder.ToTable("ProviderSkills");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProviderProfileId)
            .IsRequired();

        builder.Property(x => x.ServiceCategoryId)
            .IsRequired();

        builder.HasOne(x => x.ServiceCategory)
            .WithMany()
            .HasForeignKey(x => x.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.ProviderProfileId,
            x.ServiceCategoryId
        })
        .IsUnique();
    }
}