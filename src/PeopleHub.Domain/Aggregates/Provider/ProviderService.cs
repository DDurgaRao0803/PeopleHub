using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Aggregates.Provider;

public class ProviderService : AuditableEntity
{
    private ProviderService()
    {
    }

    public ProviderService(
        Guid providerProfileId,
        Guid serviceCategoryId,
        string title,
        string description,
        decimal basePrice,
        int estimatedDurationMinutes)
    {
        ProviderProfileId = providerProfileId;
        ServiceCategoryId = serviceCategoryId;

        UpdateDetails(
            title,
            description,
            estimatedDurationMinutes);

        ChangePrice(basePrice);

        IsActive = true;
    }

    public Guid ProviderProfileId { get; private set; }

    public Guid ServiceCategoryId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal BasePrice { get; private set; }

    public int EstimatedDurationMinutes { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Properties
    public ProviderProfile ProviderProfile { get; private set; } = null!;

    public ServiceCategory ServiceCategory { get; private set; } = null!;

    public void UpdateDetails(
        string title,
        string description,
        int estimatedDurationMinutes)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Service title is required.", nameof(title));

        if (estimatedDurationMinutes <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(estimatedDurationMinutes),
                "Estimated duration must be greater than zero.");

        Title = title.Trim();
        Description = description?.Trim() ?? string.Empty;
        EstimatedDurationMinutes = estimatedDurationMinutes;
    }

    public void ChangePrice(decimal basePrice)
    {
        if (basePrice < 0)
            throw new ArgumentOutOfRangeException(
                nameof(basePrice),
                "Base price cannot be negative.");

        BasePrice = basePrice;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}