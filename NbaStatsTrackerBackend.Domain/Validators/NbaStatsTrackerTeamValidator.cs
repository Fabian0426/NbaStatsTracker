using FluentValidation;
using NbaStatsTrackerBackend.Domain.Entities;

namespace NbaStatsTrackerBackend.Domain.Validators
{
    public sealed class TeamValidator : AbstractValidator<Team>
    {
        public TeamValidator()
        {
            RuleFor(team => team.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(team => team.Conference)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(team => team.Division)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(team => team.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(team => team.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(team => team.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(team => team.Abbreviation)
                .MaximumLength(50);
        }
    }
}