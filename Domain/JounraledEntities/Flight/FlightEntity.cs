using Domain.JounraledEntities.Flight.Events;
using Domain.SeedWork;

namespace Domain.JounraledEntities.Flight
{
    public class FlightEntity : BaseJournaledEntity
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
		public string Origin { get; private set; }

		/// <summary>
		/// Пункт назначения.
		/// </summary>
		public string Destination { get; private set; }

		/// <summary>
		/// Дата отправления.
		/// </summary>
		public DateTimeOffset Departure { get; private set; }

		/// <summary>
		/// Дата прибытия.
		/// </summary>
		public DateTimeOffset Arrival { get; private set; }

		/// <summary>
		/// Статус.
		/// </summary>
		public  StatusCode Status { get; private set; }

		#region Apply methods

		public void Apply(FlightCreatedEvent @event)
		{
			this.Id = Guid.NewGuid();
			@event.EntityId = this.Id;
			this.Origin = @event.Origin;
			this.Destination = @event.Destination;
			this.Departure = @event.Departure;
			this.Arrival = @event.Arrival;
			this.Status = @event.Status;
		}

		public void Apply(FlightChangedEvent @event)
		{
			this.Status = @event.Status;
		}
		#endregion
	}
}
