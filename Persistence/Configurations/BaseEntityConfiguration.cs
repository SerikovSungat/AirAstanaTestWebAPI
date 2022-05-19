using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
	public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
	{
		public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(k => k.Id);
			builder.Property(p => p.Created).IsRequired();
			builder.Property(p => p.Updated).IsRequired();
			builder.Property(p => p.Xmin)
				.HasColumnName("xmin")
				.HasColumnType("xid")
				.ValueGeneratedOnAddOrUpdate()
				.IsConcurrencyToken();
		}
	}
}
