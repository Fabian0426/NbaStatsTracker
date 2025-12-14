namespace NbaStatsTrackerBackend.Application.UseCases.GetASpecificTeam
{
    public record GetASpecificTeamResponse(IReadOnlyList<NbaStatsTrackerBackend.Domain.Entities.NbaTeams> Teams);
}


