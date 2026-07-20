using System.ComponentModel.DataAnnotations;

namespace PeopleHub.Contracts.Providers.Profiles;

public sealed record CreateProviderRequest(
    [Required]
    [MaxLength(200)]
    string BusinessName,

    [Required]
    [MaxLength(100)]
    string ContactPerson,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [Phone]
    string PhoneNumber,

    [Required]
    Guid ServiceCategoryId);