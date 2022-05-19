using Domain.SeedWork;

namespace Domain.JounraledEntities.Flight.Events
{
    public class FlightChangedEvent : BaseEntityEvent
    {
		/// <summary>
		/// Статус.
		/// </summary>
		public StatusCode Status { get; set; }
	}
}
