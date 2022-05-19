using Domain.JounraledEntities.User.Events;
using Domain.SeedWork;

namespace Domain.JounraledEntities.User
{
	public class UserRefreshTokenEntity : BaseJournaledEntity
	{
		/// <summary>
		/// Токен
		/// </summary>
		public string Token { get; private set; }

		/// <summary>
		/// Токен обновления
		/// </summary>
		public string RefreshToken { get; private set; }

		/// <summary>
		/// Дата и время создания
		/// </summary>
		public DateTimeOffset Created { get; private set; }

		/// <summary>
		/// Дата и время действия
		/// </summary>
		public DateTimeOffset Expires { get; private set; }

		#region

		public void Apply(UserRefreshTokenCreatedEvent @event)
		{
			this.Id = Guid.NewGuid();
			@event.EntityId = this.Id;
			this.Token = @event.Token;
			this.RefreshToken = @event.RefreshToken;
			this.Created = @event.Created;
			this.Expires = @event.Expires;
		}

		public void Apply(UserRefreshTokenChangedEvent @event)
		{
			this.Token = @event.Token;
			this.RefreshToken = @event.RefreshToken;
			this.Created = @event.Created;
			this.Expires = @event.Expires;
		}

		#endregion
	}
}
