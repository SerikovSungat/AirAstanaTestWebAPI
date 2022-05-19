using Domain.Entities.JournalEntities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		public virtual DbSet<JournalEvent> JournalEvents { get; set; }
	}
}
