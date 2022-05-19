using Domain.Entities.JournalEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
	public class JournalEventConfiguration : IEntityTypeConfiguration<JournalEvent>
	{
		public void Configure(EntityTypeBuilder<JournalEvent> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.EventName).IsRequired().HasMaxLength(1024);
			builder.Property(p => p.EntityId).IsRequired();
			builder.Property(p => p.Version).IsRequired();
			builder.Property(p => p.EntityCreated).IsRequired();
			builder.Property(p => p.EventDateTime).IsRequired();
			builder.Property(p => p.EventJson).IsRequired();
			builder.HasIndex(p => p.EntityId);
		}
	}
}
