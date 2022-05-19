using Domain.JounraledEntities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.UserConfigurations
{
	public class UserConfiguration : BaseJournaledEntityConfiguration<UserEntity>
	{
		public override void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.UserName).IsRequired().HasMaxLength(256);
			builder.HasIndex(p => p.UserName).IsUnique();
			builder.Property(p => p.Password).IsRequired().HasMaxLength(256);
			builder.HasOne(p => p.RoleEntity)
			.WithMany()
			.HasForeignKey(p => p.RoleId)
			.IsRequired();
			builder.Navigation(p => p.RoleEntity).AutoInclude();
		}
	}
}
