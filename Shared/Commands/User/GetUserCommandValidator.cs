using FluentValidation;

namespace Shared.Commands.User
{
	public class GetUserCommandValidator : AbstractValidator<GetUserCommand>
	{
		public GetUserCommandValidator()
		{
			this.RuleFor(m => m.UserName).NotEmpty();
			this.RuleFor(m => m.Password).NotEmpty();
		}
	}
}
