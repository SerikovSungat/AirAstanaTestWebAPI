using System.Reflection;
using Application.Options;
using Persistence.Options;
using AirAstanaWebAPI.Constants;
using AirAstanaWebAPI.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using AirAstanaWebAPI.Options.Validators;
using WebApi.OpenApi;

namespace WebApi.Extensions
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Configures the settings and secrets by binding the contents of the files and remote sources
		/// to the specified POCO and adding <see cref="IOptions{T}"/> objects to the services collection.
		/// </summary>
		/// <param name="services">The services collection.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>The same services collection.</returns>
		public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IValidateOptions<IdempotencyControlOptions>, IdempotencyControlOptionsValidator>()
				.AddSingleton<IValidateOptions<ApiSwaggerOptions>, ApiSwaggerOptionsValidator>();

			services
				.ConfigureAndValidateSingleton<ApplicationOptions>(configuration, o => o.BindNonPublicProperties = false)
				.ConfigureAndValidateSingleton<KestrelServerOptions>(configuration.GetSection(nameof(ApplicationOptions.Kestrel)), o => o.BindNonPublicProperties = false)
				.ConfigureAndValidateSingleton<IdempotencyControlOptions>(configuration.GetSection(nameof(ApplicationOptions.IdempotencyControl)), o => o.BindNonPublicProperties = false)
				.ConfigureAndValidateSingleton<ApiSwaggerOptions>(configuration.GetSection(nameof(ApplicationOptions.ApiSwagger)), o => o.BindNonPublicProperties = false)
				.ConfigureAndValidateSingleton<AuthenticationOptions>(configuration.GetSection(nameof(ApplicationOptions.Authentication)), o => o.BindNonPublicProperties = false)
				.ConfigureAndValidateSingleton<PersistenceOptions>(configuration.GetSection(nameof(ApplicationOptions.Persistence)), o => o.BindNonPublicProperties = false);

			return services;
		}

		/// <summary>
		/// Creates instances of all settings and gets values to perform the validation process at application startup.
		/// </summary>
		/// <param name="services">The services collection.</param>
		/// <returns>The same services collection.</returns>
		public static IServiceCollection AddOptionsAndSecretsValidation(this IServiceCollection services)
		{
			try
			{
				IServiceProvider serviceProvider = services.BuildServiceProvider();

				ApplicationOptions applicationOptions = serviceProvider.GetService<IOptions<ApplicationOptions>>().Value;
				IdempotencyControlOptions idempotencyControlOptions = serviceProvider.GetService<IOptions<IdempotencyControlOptions>>().Value;
				ApiSwaggerOptions apiSwaggerOptions = serviceProvider.GetService<IOptions<ApiSwaggerOptions>>().Value;
				AuthenticationOptions authenticationOptions = serviceProvider.GetService<IOptions<AuthenticationOptions>>().Value;
				PersistenceOptions persistenceOptions = serviceProvider.GetService<IOptions<PersistenceOptions>>().Value;
			}
			catch (OptionsValidationException ex)
			{
				string message = $"Error validating {ex.OptionsType.FullName}: {string.Join(", ", ex.Failures)}.";
				Log.Error(ex, message);
				Console.WriteLine(message);
				throw;
			}
			return services;
		}

		/// <summary>
		/// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
		/// https://docs.asp.net/en/latest/security/cors.html
		/// </summary>
		public static IServiceCollection AddCustomCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(
					CorsPolicyName.AllowAny,
					x => x
						.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});

			return services;
		}

		/// <summary>
		/// Add custom routing settings which determines how URL's are generated.
		/// </summary>
		public static IServiceCollection AddCustomRouting(this IServiceCollection services)
		{
			services.AddRouting(o =>
			{
				o.LowercaseUrls = true;
			});

			return services;
		}

		public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
		{
			// TODO Not implemented.
			return services;
		}

		public static IServiceCollection AddCustomSwagger(this IServiceCollection services, Assembly assembly)
		{
			services.AddCustomApiVersioning();

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>(serviceProvider =>
				{
					return new SwaggerConfigureOptions(
						serviceProvider.GetService<IApiVersionDescriptionProvider>(),
						assembly: assembly,
						idempotencyControlOptions: serviceProvider.GetService<IOptions<IdempotencyControlOptions>>());
				})
				.AddSwaggerGen()
				.AddFluentValidationRulesToSwagger();

			return services;
		}

		private static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
		{
			services.AddApiVersioning(o =>
				{
					o.AssumeDefaultVersionWhenUnspecified = true;
					o.ReportApiVersions = true;
					//TODO Change to UrlSegmentApiVersionReader
					o.ApiVersionReader = new UrlSegmentApiVersionReader();
				})
				.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV"); // Version format: 'v'major[.minor][-status]
			return services;
		}

		#region Custom extensions

		public static T GetService<T>(this IServiceProvider serviceProvider) where T : class
		{
			return serviceProvider.GetService(typeof(T)) as T;
		}


		/// <summary>
		/// Registers <see cref="IOptions{TOptions}"/> and <typeparamref name="TOptions"/> to the services container.
		/// Also runs data annotation validation.
		/// </summary>
		/// <typeparam name="TOptions">The type of the options.</typeparam>
		/// <param name="services">The services collection.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configureBinder">Used to configure the binder options.</param>
		/// <returns>The same services collection.</returns>
		private static IServiceCollection ConfigureAndValidateSingleton<TOptions>(
			this IServiceCollection services,
			IConfiguration configuration,
			Action<BinderOptions> configureBinder)
			where TOptions : class, new()
		{
			if (services is null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			services
				.AddOptions<TOptions>()
				.Bind(configuration, configureBinder)
				.ValidateDataAnnotations();
			return services.AddSingleton(x => x.GetRequiredService<IOptions<TOptions>>().Value);
		}

		#endregion
	}
}
