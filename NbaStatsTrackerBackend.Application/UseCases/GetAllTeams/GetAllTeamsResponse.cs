namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams
{
    public record GetAllTeamsResponse(IReadOnlyList<NbaStatsTrackerBackend.Domain.Entities.Players> Teams);
}
