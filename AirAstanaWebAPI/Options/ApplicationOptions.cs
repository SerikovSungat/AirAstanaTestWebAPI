using System.ComponentModel.DataAnnotations;
using Application.Options;
using Persistence.Options;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AirAstanaWebAPI.Options
{
	/// <summary>
	/// Конфигурация приложения.
	/// </summary>
	public class ApplicationOptions
	{
		/// <summary>
		/// Конфигурация веб-сервера Kestrel.
		/// </summary>
		[Required]
		public KestrelServerOptions Kestrel { get; set; }

		/// <summary>
		/// Фильтр идемпотентности.
		/// </summary>
		[Required]
		public IdempotencyControlOptions IdempotencyControl { get; set; }

		/// <summary>
		/// Конфигурация Swagger.
		/// </summary>
		[Required]
		public ApiSwaggerOptions ApiSwagger { get; set; }

		/// <summary>
		/// Конфигурация аутентификации.
		/// </summary>
		[Required]
		public AuthenticationOptions Authentication { get; set; }

		/// <summary>
		/// Конфигурация БД.
		/// </summary>
		[Required]
		public PersistenceOptions Persistence { get; set; }
	}
}
