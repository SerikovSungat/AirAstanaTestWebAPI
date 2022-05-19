using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
	public static class Startup
	{
		public static IServiceCollection AddDbDataSharedLayer(this IServiceCollection services)
		{
			return services;
		}
	}
}
