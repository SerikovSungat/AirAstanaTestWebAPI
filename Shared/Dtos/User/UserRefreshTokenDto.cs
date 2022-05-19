namespace Shared.Dtos.User
{
	public class UserRefreshTokenDto
	{
		/// <summary>
		/// Токен
		/// </summary>
		public string Token { get; private set; }

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
