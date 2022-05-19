using Logging.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Logging
{
	public static class Startup
	{
		public static IServiceCollection AddDbDataLogging(this IServiceCollection services,
			Action<HttpContextLoggingOptions>? httpContextOptions = null,
			Action<IdempotencyLoggingOptions>? idempotencyOptions = null)
		{
			if (httpContextOptions is not null)
			{
				services.Configure(httpContextOptions);
			}

			if (idempotencyOptions is not null)
			{
				services.Configure(idempotencyOptions);
			}

			services.AddOptions();

			return services;
		}

		[Obsolete]
		public static IApplicationBuilder UseDbDataLogging(this IApplicationBuilder app)
		{
			app.UseNetworkLogging()
				.UseHttpContextLogging()
				.UseIdempotencyLogging();

			return app;
		}

		public static IApplicationBuilder UseNetworkLogging(this IApplicationBuilder app)
		{
			app.UseMiddleware<NetworkLoggingMiddleware>();
			return app;
		}

		public static IApplicationBuilder UseHttpContextLogging(this IApplicationBuilder app)
		{
			app.UseMiddleware<HttpContextLoggingMiddleware>();
			return app;
		}

		public static IApplicationBuilder UseIdempotencyLogging(this IApplicationBuilder app)
		{
			app.UseMiddleware<IdempotencyLoggingMiddleware>();
			return app;
		}
	}
}
