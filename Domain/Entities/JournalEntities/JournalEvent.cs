namespace Domain.Entities.JournalEntities
{
	public class JournalEvent
	{
		public uint Id { get; set; }
		public Guid EntityId { get; set; }
		public DateTime EntityCreated { get; set; }
		public DateTime EventDateTime { get; set; }
		public int Version { get; set; }
		public string EventName { get; set; }
		public string EventJson { get; set; }
	}
}
