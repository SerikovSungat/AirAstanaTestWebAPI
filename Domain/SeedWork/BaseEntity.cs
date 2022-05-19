namespace Domain.SeedWork
{
	/// <summary>
	/// Базовый класс для сущностей
	/// </summary>
	public abstract class BaseEntity
	{
		/// <summary>
		/// Первичный ключ записи. 
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// Дата и время создания 
		/// </summary>
		public DateTime Created { get; protected set; } = DateTime.Now;
		/// <summary>
		/// Дата и время последнего изменения
		/// </summary>
		public DateTime Updated { get; protected set; }

		/// <summary>
		/// Данное поле используется для контроля версии https://www.npgsql.org/efcore/modeling/concurrency.html
		/// </summary>
		public uint Xmin { get; set; }
	}
}
