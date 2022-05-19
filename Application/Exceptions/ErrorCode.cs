using System.ComponentModel;

namespace Application.Exceptions
{
	/// <summary>
	/// Коды ошибок приложения.
	/// </summary>
	public enum ErrorCode
	{
		/// <summary>
		/// Неизвестный код ошибки.
		/// </summary>
		[Description("Неизвестный код ошибки.")]
		Unknown = 0,

		/// <summary>
		/// Неверная версия события. 
		/// </summary>
		[Description("Неверная версия события.")]
		WrongEventVersion = 1000,

		/// <summary>
		/// Сущность не найден.
		/// </summary>
		[Description("Сущность не найден.")]
		EntityNotFound = 1001,

		/// <summary>
		/// Сущность был удален.
		/// </summary>
		[Description("Сущность был удален.")]
		EntityHasBeenDeleted = 1002,

		/// <summary>
		///Пользователь не найден.
		/// </summary>
		[Description("Пользователь не найден.")]
		UserNotFound = 1003,

		/// <summary>
		///Пользователь не найден.
		/// </summary>
		[Description("У пользователя нет доступа.")]
		UserNotAuthorized = 1004,
	}
}
