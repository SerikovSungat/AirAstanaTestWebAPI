using System;
using System.Threading;
using Application.Abstract.Repositories;
using System.Threading.Tasks;
using Domain.JounraledEntities.User;
using Microsoft.Extensions.DependencyInjection;
using Domain.JounraledEntities.Flight;

namespace Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IServiceProvider serviceProvider;

		public UnitOfWork(ApplicationDbContext applicationDbContext, IServiceProvider serviceProvider)
		{
			this.applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
			this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		public IJournaledGenericRepository<UserEntity> UserRepository => this.serviceProvider.GetRequiredService<IJournaledGenericRepository<UserEntity>>();
		public IJournaledGenericRepository<FlightEntity> FlightRepository => this.serviceProvider.GetRequiredService<IJournaledGenericRepository<FlightEntity>>();
		public IJournaledGenericRepository<UserRefreshTokenEntity> UserRefreshTokenRepository => this.serviceProvider.GetRequiredService<IJournaledGenericRepository<UserRefreshTokenEntity>>();

		public void Commit()
		{
			this.applicationDbContext.SaveChanges();
		}

		public Task CommitAsync(CancellationToken cancellationToken)
		{
			return this.applicationDbContext.SaveChangesAsync(cancellationToken);		
		}
	}
}
