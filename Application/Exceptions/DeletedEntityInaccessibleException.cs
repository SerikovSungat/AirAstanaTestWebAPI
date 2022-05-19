using System.Runtime.Serialization;

namespace Application.Exceptions
{
    /// <summary>
    /// Представляет исключение, которое выдается при попытке выполнить удаление с уже удаленным объектом <see cref="BaseEntity"/>.
    /// </summary>
    public class DeletedEntityInaccessibleException : ApplicationExceptionBase
	{
		public DeletedEntityInaccessibleException(Guid entityId, SerializationInfo info, StreamingContext context) : base(info, context, ErrorCode.EntityHasBeenDeleted)
		{
			this.EntityId = entityId;
		}

		public DeletedEntityInaccessibleException(Guid entityId, string message = "Сущность был удален.") : base(message, ErrorCode.EntityHasBeenDeleted)
		{
			this.EntityId = entityId;
		}

		public DeletedEntityInaccessibleException(Guid entityId, string message, Exception innerException) : base(message, innerException, ErrorCode.EntityHasBeenDeleted)
		{
			this.EntityId = entityId;
		}

		public Guid EntityId { get; }
	}
}
