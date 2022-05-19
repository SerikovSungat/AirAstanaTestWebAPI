using Domain.JounraledEntities.Flight;
using MediatR;

namespace Shared.Commands.Flight
{
    public class CreateFlightCommand : IRequest<int>
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
