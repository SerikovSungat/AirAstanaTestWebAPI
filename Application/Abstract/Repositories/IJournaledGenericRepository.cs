using Domain.SeedWork;
using System.Linq.Expressions;
namespace Application.Abstract.Repositories
{
	public interface IJournaledGenericRepository<TEntity> where TEntity : BaseJournaledEntity
	{
		ValueTask<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken,
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string? includeProperties = null,
			int skip = 0,
			int take = 0);

		/// <summary>
		/// Getting entity Collection. 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="filter"></param>
		/// <param name="orderBy"></param>
		/// <param name="includeProperties"></param>
		/// <param name="skip">skip parameter for pagination</param>
		/// <param name="take">take parameter for pagination</param>
		/// <returns></returns>
		ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
		IQueryable<TEntity> GetAll();
		Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? filter = null);
		Task<int> RaiseEvent(BaseEntityEvent @event, CancellationToken cancellationToken, bool autoCommit = true);
	}
}
