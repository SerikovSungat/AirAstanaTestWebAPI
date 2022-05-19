namespace Domain.JounraledEntities.Flight
{
	/// <summary>
	/// Коды статусов.
	/// </summary>
	public enum StatusCode
	{
		/// <summary>
		/// Во время.
		/// </summary>
		InTime = 1,

		/// <summary>
		/// Задерживается. 
		/// </summary>
		Delayed = 2,

		/// <summary>
		/// Отменен.
		/// </summary>
		Cancelled = 3
	}
}
