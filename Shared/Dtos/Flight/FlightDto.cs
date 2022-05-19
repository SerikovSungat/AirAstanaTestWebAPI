using Domain.JounraledEntities.Flight;

namespace Shared.Dtos.Flight
{
    public class FlightDto
    {
		public Guid Id { get; set; }
		/// <summary>
		/// Источник.
		/// </summary>
		/// 
		public int Version { get; set; }
		/// <summary>
		/// Источник.
		/// </summary>
		/// 
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
