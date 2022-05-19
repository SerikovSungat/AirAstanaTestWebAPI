using Domain.JounraledEntities.Flight;
using Domain.JounraledEntities.User;

namespace Application.Abstract.Repositories
{
	public interface IUnitOfWork
	{
		void Commit();
		Task CommitAsync(CancellationToken cancellationToken);
		IJournaledGenericRepository<UserEntity> UserRepository { get; }
		IJournaledGenericRepository<UserRefreshTokenEntity> UserRefreshTokenRepository { get; }
		IJournaledGenericRepository<FlightEntity> FlightRepository { get; }
	}
}
