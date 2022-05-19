namespace Shared.Dtos.User
{
	public class UserDto
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Token
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// RefreshToken
		/// </summary>
		public string RefreshToken { get; set; }

		/// <summary>
		/// Список ролей
		/// </summary>
		public string Role { get; set; }
	}
}
