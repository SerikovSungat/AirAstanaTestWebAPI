using Domain.JounraledEntities.Role;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.RoleConfigurations
{
    public class RoleConfiguration : BaseJournaledEntityConfiguration<RoleEntity>
    {
		public override void Configure(EntityTypeBuilder<RoleEntity> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.Code).IsRequired().HasMaxLength(256);
			builder.HasIndex(p => p.Code).IsUnique();
		}
	}
}
