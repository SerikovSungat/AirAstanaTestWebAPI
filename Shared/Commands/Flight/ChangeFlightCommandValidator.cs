using FluentValidation;

namespace Shared.Commands.Flight
{
    public class ChangeFlightCommandValidator : AbstractValidator<ChangeFlightCommand>
    {
        public ChangeFlightCommandValidator()
        {
            this.RuleFor(m => m.Status)
                .NotEmpty();
        }
    }
}
