using Domain.JounraledEntities.Role;
using Domain.SeedWork;

namespace Domain.JounraledEntities.User
{
	public class UserEntity : BaseJournaledEntity
	{
		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// Пароль пользователя
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// ID роля пользователя
		/// </summary>
		public Guid RoleId { get; private set; }

		/// <summary>
		/// Способ оплаты.
		/// </summary>
		public RoleEntity RoleEntity { get; private set; }
	}
}