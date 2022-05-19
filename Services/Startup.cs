using CorrelationId;
using CorrelationId.DependencyInjection;
using Application.Abstract.Services;
using Application.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
	public static class Startup
	{
		public static IServiceCollection AddDbDataServices(this IServiceCollection services, string correlationIdHeader)
		{
			services.AddCorrelationIdFluent(correlationIdHeader);
			services.AddHttpContextAccessor()
				.AddScoped<IRuntimeContextAccessor, RuntimeContextAccessor>()
				.AddTransient<IProblemDetailsFactory, AppProblemDetailsFactory>();

			return services;
		}

		public static IApplicationBuilder UseDbDataServices(this IApplicationBuilder appBuilder)
		{
			appBuilder.UseCorrelationId();

			return appBuilder;
		}

		private static IServiceCollection AddCorrelationIdFluent(this IServiceCollection services, string correlationIdHeader)
		{
			services.AddDefaultCorrelationId(c =>
			{
				c.CorrelationIdGenerator = () => Guid.NewGuid().ToString();
				c.AddToLoggingScope = true;
				c.LoggingScopeKey = Logs.CorrelationId;
				c.EnforceHeader = false;
				c.IgnoreRequestHeader = true;
				c.IncludeInResponse = true;
				c.RequestHeader = correlationIdHeader;
				c.ResponseHeader = correlationIdHeader;
				c.UpdateTraceIdentifier = false;
			});

			return services;
		}
	}
}
