using FluentValidation;
using NbaStatsTrackerBackend.Domain.Entities;

namespace NbaStatsTrackerBackend.Domain.Validators
{
    public sealed class PlayerValidator : AbstractValidator<Players>
    {
        public PlayerValidator()
        {
            RuleFor(team => team.Id)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(team => team.FirstName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(team => team.LastName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(team => team.Position)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(team => team.Height)
                .MaximumLength(50);
            RuleFor(team => team.Weight)
                .MaximumLength(50);
            RuleFor(team => team.JerseyNumber)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(team => team.College)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(team => team.Country)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}