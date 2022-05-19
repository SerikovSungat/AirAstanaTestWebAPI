using System.Runtime.Serialization;

namespace Application.Exceptions
{
	public class UserNotFoundException : ApplicationExceptionBase
	{
		public UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context, ErrorCode.UserNotFound)
		{
		}

		public UserNotFoundException(string message = "Пользователь не найден.") : base(message, ErrorCode.UserNotFound)
		{
		}

		public UserNotFoundException(string message, Exception innerException) : base(message, innerException, ErrorCode.UserNotFound)
		{
		}
	}
}
