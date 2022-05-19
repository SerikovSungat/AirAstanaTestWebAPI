using System.Runtime.Serialization;

namespace Application.Exceptions
{
	public class UserNotAuthorizedException : ApplicationExceptionBase
    {
		public UserNotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context, ErrorCode.UserNotAuthorized)
		{
		}

		public UserNotAuthorizedException(string message = "Неправильный логин или пароль.") : base(message, ErrorCode.UserNotAuthorized)
		{
		}

		public UserNotAuthorizedException(string message, Exception innerException) : base(message, innerException, ErrorCode.UserNotAuthorized)
		{
		}
	}
}
