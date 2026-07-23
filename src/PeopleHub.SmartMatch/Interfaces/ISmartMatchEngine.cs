using PeopleHub.Domain.Entities;
using PeopleHub.SmartMatch.Models;

namespace PeopleHub.SmartMatch.Interfaces;

public interface ISmartMatchEngine
{
    SmartMatchResult FindBestMatch(
        ServiceRequest serviceRequest,
        IReadOnlyCollection<SmartMatchCandidate> candidates);
}