using Domain.JounraledEntities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.UserConfigurations
{
	public class UserRefreshTokenConfiguration : BaseJournaledEntityConfiguration<UserRefreshTokenEntity>
	{
		public override void Configure(EntityTypeBuilder<UserRefreshTokenEntity> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.Token).IsRequired();
			builder.Property(p => p.RefreshToken).IsRequired();
			builder.Property(p => p.Created).IsRequired();
			builder.Property(p => p.Expires).IsRequired();
		}
	}
}
