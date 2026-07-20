using System.ComponentModel.DataAnnotations;

namespace PeopleHub.Contracts.Providers.Profiles;

public sealed class UpdateProviderProfileRequest
{
    [Required]
    [MaxLength(1000)]
    public string Bio { get; init; } = string.Empty;

    [Range(0, 100)]
    public int ExperienceYears { get; init; }
}