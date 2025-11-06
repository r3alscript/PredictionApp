using FluentValidation;
using PredictionApp.Domain.Entities;

namespace PredictionApp.Domain.Validators
{
    public class PredictionValidator : AbstractValidator<Prediction>
    {
        public PredictionValidator()
        {
            RuleFor(p => p.Message)
                .NotEmpty().WithMessage("Prediction message cannot be empty.")
                .MinimumLength(3).WithMessage("Prediction message must be at least 3 characters long.");

            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("Prediction ID must be greater than zero.");
        }
    }
}
