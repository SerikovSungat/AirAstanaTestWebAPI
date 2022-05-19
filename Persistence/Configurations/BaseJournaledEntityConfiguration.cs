using Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
	public class BaseJournaledEntityConfiguration<TAggregate> : BaseEntityConfiguration<TAggregate> where TAggregate : BaseJournaledEntity
	{
		public override void Configure(EntityTypeBuilder<TAggregate> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.Version).IsRequired();
		}
	}
}
