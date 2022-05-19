using Domain.JounraledEntities.Flight;
using MediatR;

namespace Shared.Commands.Flight
{
    public class ChangeFlightCommand : IRequest<int>
    {
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Версия.
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// Статус.
		/// </summary>
		public StatusCode Status { get; set; }
	}
}
