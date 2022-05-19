namespace Domain.SeedWork
{
	/// <summary>
	/// Базовый класс для журналируемых агрегатов
	/// </summary>
	public abstract class BaseJournaledEntity : BaseEntity
	{
		/// <summary>
		/// Текущая версия агрегата. Увеличивается по мере происходящих событий.
		/// </summary>
		public int Version { get; protected set; }
		/// <summary>
		/// Базовый метод для приминения события к агрегату
		/// </summary>
		/// <param name="event"></param>
		public void Apply(BaseEntityEvent @event) { }

		/// <summary>
		/// Инкрементация версии агрегата.
		/// </summary>
		public void IncrementVersion()
		{
			this.Version += 1;
		}
	}
}
