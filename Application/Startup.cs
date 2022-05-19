using Application.Configurations;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Application
{
	public static class Startup
	{
		public static IServiceCollection AddDbDataApplication(this IServiceCollection services)
		{
			services.AddMediatR(typeof(MappingProfile).Assembly);
			services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}
	}
}
