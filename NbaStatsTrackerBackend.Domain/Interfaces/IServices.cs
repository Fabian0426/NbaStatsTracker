using NbaStatsTrackerBackend.Domain.Entities;

namespace NbaStatsTrackerBackend.Domain.Interfaces
{
    public interface IServices
    {
        Task<IReadOnlyList<Teams>> GetAllTeamsAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Players>> GetPlayersAsync(int? cursor, int perPage, string? search, CancellationToken cancellationToken);
    }
}