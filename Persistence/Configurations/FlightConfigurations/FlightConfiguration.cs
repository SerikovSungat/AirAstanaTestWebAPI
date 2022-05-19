using Domain.JounraledEntities.Flight;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Persistence.Configurations.FlightConfigurations
{
	public class FlightConfiguration : BaseJournaledEntityConfiguration<FlightEntity>
	{
		public override void Configure(EntityTypeBuilder<FlightEntity> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.Origin).IsRequired().HasMaxLength(256);
			builder.Property(p => p.Destination).IsRequired().HasMaxLength(256);
			builder.Property(p => p.Departure).IsRequired().HasMaxLength(100);
			builder.Property(p => p.Arrival).IsRequired().HasMaxLength(100);
			builder.Property(p => p.Status).IsRequired().HasMaxLength(20).HasConversion(p=>p.ToString(),p=>(StatusCode)Enum.Parse(typeof(StatusCode), p));
		}
	}
}
