using Domain.SeedWork;

namespace Domain.JounraledEntities.Flight.Events
{
    public class FlightCreatedEvent : BaseEntityEvent
    {
		/// <summary>
		/// Источник.
		/// </summary>
		public string Origin { get; set; }

		/// <summary>
		/// Пункт назначения.
		/// </summary>
		public string Destination { get; set; }

		/// <summary>
		/// Дата отправления.
		/// </summary>
		public DateTimeOffset Departure { get; set; }

		/// <summary>
		/// Дата прибытия.
		/// </summary>
		public DateTimeOffset Arrival { get; set; }

		/// <summary>
		/// Статус.
		/// </summary>
		public StatusCode Status { get; set; }
	}
}
