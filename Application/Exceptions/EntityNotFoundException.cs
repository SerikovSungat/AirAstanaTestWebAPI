using System.Runtime.Serialization;

namespace Application.Exceptions
{
	/// <summary>
	/// Представляет исключение, которое выдается при попытке получить запрашиваемый сущность  <see cref="BaseEntity"/> не существует.
	/// </summary>
	public class EntityNotFoundException : ApplicationExceptionBase
	{
		public EntityNotFoundException(Guid entityId, SerializationInfo info, StreamingContext context) : base(info, context, ErrorCode.EntityNotFound)
		{
			this.EntityId = entityId;
		}

		public EntityNotFoundException(Guid entityId, string message = "Сущность не найден.") : base(message, ErrorCode.EntityNotFound)
		{
			this.EntityId = entityId;
		}

		public EntityNotFoundException(Guid entityId, string message, Exception innerException) : base(message, innerException, ErrorCode.EntityNotFound)
		{
			this.EntityId = entityId;
		}

		public Guid EntityId { get; }
	}
}
