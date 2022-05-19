namespace Domain.SeedWork
{
	/// <summary>
	/// Базовый класс для событий для журналируемых агрегатов
	/// </summary>
	public abstract class BaseEntityEvent
	{
		/// <summary>
		/// Ид агрегата к которому отностися текущее событие
		/// </summary>
		public Guid EntityId { get; set; }
		/// <summary>
		/// Текущая версия агрегата
		/// </summary>
		public int Version { get; set; }
		public DateTime EventDateTime { get; private set; }
		public string EventName { get; private set; }
		public BaseEntityEvent()
		{
			this.EventName = this.GetType().Name;
			this.EventDateTime = DateTime.Now;
		}
	}
}
