using MediatR;

namespace Shared.Commands.User
{
	public class CreateUserRefreshTokenCommand : IRequest<int>
	{
		/// <summary>
		/// Токен
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Токен обновления
		/// </summary>
		public string RefreshToken { get; set; }

		/// <summary>
		/// Дата и время создания
		/// </summary>
		public DateTimeOffset Created { get; set; }

		/// <summary>
		/// Дата и время действия
		/// </summary>
		public DateTimeOffset Expires { get; set; }
	}
}
