using System.Runtime.Serialization;

namespace Application.Exceptions
{
	public class WrongEventVersionException : ApplicationExceptionBase
	{
		public WrongEventVersionException(SerializationInfo info, StreamingContext context) : base(info, context, ErrorCode.WrongEventVersion)
		{

		}

		public WrongEventVersionException(string message = "Неверная версия события.") : base(message, ErrorCode.WrongEventVersion)
		{

		}

		public WrongEventVersionException(string message, Exception innerException) : base(message, innerException, ErrorCode.WrongEventVersion)
		{

		}
	}
}
