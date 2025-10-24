using FluentValidation;

namespace NbaStatsTrackerBackend.Application.UseCases.GetAllTeams
{
    public class GetAllTeamsValidator : AbstractValidator<GetAllTeamsRequest>
    {
        public GetAllTeamsValidator() {
            RuleFor(x => x.conference)
                .Must(c => c == "East" || c == "West");
        }
    }
}
