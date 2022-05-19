using System.Reflection;
using System.Text;
using AirAstanaWebAPI.Constants;
using AirAstanaWebAPI.Extensions;
using AirAstanaWebAPI.Middlewares;
using AirAstanaWebAPI.Middlewares.ResourceFilter;
using AirAstanaWebAPI.Options;
using Application;
using Logging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Primitives;
using Persistence;
using Persistence.Options;
using Services;
using Shared;
using WebApi.Extensions;

namespace AirAstanaWebAPI
{
    public sealed class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment, Assembly assembly)
		{
			this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.Environment = environment ?? throw new ArgumentNullException(nameof(environment));
			this.Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }
		public Assembly Assembly { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCustomOptions(this.Configuration)
				.AddOptionsAndSecretsValidation();

			services.AddDbDataServices("x-correlation-id")
				.AddCustomCors()
				.AddCustomRouting()
				.AddCustomHealthChecks()
				.AddCustomSwagger(this.Assembly);

			ApplicationOptions appSettings = this.Configuration.Get<ApplicationOptions>();
			services.AddDbDataLogging(httpContextOptions: options =>
			{
				options.LogRequestBody = true;
				options.LogResponseBody = true;
				options.MaxBodyLength = 32000;
				options.SkipPaths = new List<PathString>()
				{
					"/authentication/token"
				};
			}, idempotencyOptions: options =>
			{
				options.IdempotencyHeader = appSettings.IdempotencyControl.ClientRequestIdHeader;
			});

			services.AddControllers()
				.AddCustomJsonOptions(this.Environment)
				.AddCustomMvcOptions(this.Configuration)
				.AddCustomModelValidation();

			PersistenceOptions persistenceOptions = this.Configuration.GetSection(nameof(ApplicationOptions.Persistence)).Get<PersistenceOptions>();
			services.AddDbDataApplication()
				.AddDbDataSharedLayer()
				.AddPersistenceLayer(persistenceOptions);

			services.AddScoped<IdempotencyFilterAttribute>();
		}

		public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
		{
			#region PreConfiguration

			lifetime.ApplicationStopping.Register(() =>
			{
				app.ApplicationServices.GetRequiredService<ILogger<Startup>>().LogInformation("Shutdown has been initiated.");
			});
			lifetime.ApplicationStopped.Register(() =>
			{
				app.ApplicationServices.GetRequiredService<ILogger<Startup>>().LogInformation("Application on stopped has been called.");
			});

			ChangeToken.OnChange(this.Configuration.GetReloadToken, () =>
			{
				app.ApplicationServices.GetRequiredService<ILogger<Startup>>().LogInformation("Options or secrets has been modified.");
			});

			#endregion

			if (this.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseDbDataServices()
				.UseForwardedHeaders(new ForwardedHeadersOptions
				{
					ForwardedHeaders = ForwardedHeaders.XForwardedFor
				});

			app.UseNetworkLogging()
				.UseCustomExceptionHandler();

			app.UseMiddleware<JwtMiddleware>();

			app.UseRouting()
				.UseCors(CorsPolicyName.AllowAny)
				.UseHttpContextLogging()
				.UseIdempotencyLogging();

			app.UseHttpsRedirection();

			app.UseAuthentication(); 

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers().RequireCors(CorsPolicyName.AllowAny);

				endpoints.MapGet("/nodeid", async (context) =>
				{
					await context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(Node.Id));
				}).RequireCors(CorsPolicyName.AllowAny);
			});

			app.UseCustomSwagger(this.Assembly);
		}
	}
}
