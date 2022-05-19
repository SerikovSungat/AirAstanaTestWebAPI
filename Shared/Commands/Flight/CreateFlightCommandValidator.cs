using FluentValidation;

namespace Shared.Commands.Flight
{
    public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightCommandValidator()
        {


            this.RuleFor(m => m.Origin)
                .NotEmpty()
                .Length(1, 256);

            this.RuleFor(m => m.Destination)
                .NotEmpty()
                .Length(1, 256);

            this.RuleFor(m => m.Arrival)
                .NotEmpty();

            this.RuleFor(m => m.Departure)
                .NotEmpty();

            this.RuleFor(m => m.Status)
                .NotEmpty();
        }
    }
}
