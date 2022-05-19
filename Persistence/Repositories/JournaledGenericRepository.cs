using Application.Abstract.Repositories;
using Domain.Entities.JournalEntities;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Application.Exceptions;

namespace Persistence.Repositories
{
	public class JournaledGenericRepository<TEntity> : IJournaledGenericRepository<TEntity> where TEntity : BaseJournaledEntity, new()
	{
		internal ApplicationDbContext applicationDbContext;
		internal DbSet<TEntity> dbSet;
		private readonly IMediator mediator;
		private readonly IMapper mapper;

		public JournaledGenericRepository(ApplicationDbContext applicationDbContext, IMediator mediator, IMapper mapper)
		{
			this.applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

			this.dbSet = applicationDbContext.Set<TEntity>();
		}
		public virtual async ValueTask<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken,
			Expression<Func<TEntity, bool>>? filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
			string? includeProperties = null,
			int skip = 0,
			int take = 0)
		{
			IQueryable<TEntity> query = this.dbSet.AsNoTracking();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (!string.IsNullOrWhiteSpace(includeProperties))
			{
				foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			if (orderBy != null)
			{
				if (take > 0)
				{
					return await orderBy(query).Skip(skip).Take(take).ToListAsync(cancellationToken);
				}
				return await orderBy(query).ToListAsync(cancellationToken);
			}
			else
			{
				if (take > 0)
				{
					return await query.Skip(skip).Take(take).ToListAsync(cancellationToken);
				}
				return await query.ToListAsync(cancellationToken);
			}
		}

		public virtual async ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			return await this.dbSet.FindAsync(new object[] { id }, cancellationToken);
		}

		public virtual IQueryable<TEntity> GetAll()
		{
			IQueryable<TEntity> query = this.dbSet;
			return query;
		}

		public virtual Task<int> CountAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? filter = null)
		{
			IQueryable<TEntity> query = this.dbSet.AsNoTracking();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.CountAsync(cancellationToken);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="event"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="autoCommit"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public virtual async Task<int> RaiseEvent(BaseEntityEvent @event, CancellationToken cancellationToken, bool autoCommit = true)
		{
			TEntity? entity = await this.dbSet.FindAsync(new object[] { @event.EntityId }, cancellationToken);

			if (entity != null)
			{
				if (@event.Version != entity.Version)
				{
					throw new WrongEventVersionException("Wrong event version.");
				}

				entity.Apply(@event as dynamic);
			}
			else
			{
				if (@event.Version != 0)
				{
					throw new WrongEventVersionException("Wrong event version.");
				}

				entity = new TEntity();
				entity.Apply(@event as dynamic);
				this.applicationDbContext.Add(entity);
			}

			entity.IncrementVersion();
			@event.Version = entity.Version;

			var journalEvent = new JournalEvent();
			journalEvent.EntityId = @event.EntityId;
			journalEvent.EventName = @event.EventName;
			journalEvent.Version = @event.Version;
			journalEvent.EntityCreated = entity.Created;
			journalEvent.EventDateTime = @event.EventDateTime;
			journalEvent.EventJson = JsonSerializer.Serialize(@event);

			this.applicationDbContext.JournalEvents.Add(journalEvent);

			if (autoCommit)
			{
				await this.applicationDbContext.SaveChangesAsync(cancellationToken);
			}

			return await Task.FromResult(0);
		}
	}
}
