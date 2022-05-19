using FluentValidation;

namespace Shared.Commands.User
{
	public class CreateUserRefreshTokenCommandValidator : AbstractValidator<CreateUserRefreshTokenCommand>
	{
		public CreateUserRefreshTokenCommandValidator()
		{
			this.RuleFor(x => x.Token).NotEmpty();
			this.RuleFor(x => x.RefreshToken).NotEmpty();
			this.RuleFor(x => x.Created).NotEmpty();
			this.RuleFor(x => x.Expires).NotEmpty();
		}
	}
}
