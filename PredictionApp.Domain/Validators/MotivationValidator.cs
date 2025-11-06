using FluentValidation;
using PredictionApp.Domain.Entities;

namespace PredictionApp.Domain.Validators
{
    public class MotivationValidator : AbstractValidator<Motivation>
    {
        public MotivationValidator()
        {
            RuleFor(m => m.Message)
                .NotEmpty().WithMessage("Motivation message cannot be empty.")
                .MinimumLength(3).WithMessage("Motivation message must be at least 3 characters long.");

            RuleFor(m => m.Id)
                .GreaterThan(0).WithMessage("Motivation ID must be greater than zero.");
        }
    }
}
