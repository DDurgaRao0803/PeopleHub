using PeopleHub.Application.SmartMatch.Models;

namespace PeopleHub.Application.SmartMatch.Interfaces;

public interface ISmartMatchService
{
    Task<SmartMatchResponse> FindBestProviderAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default);
}