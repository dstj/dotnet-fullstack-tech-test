using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fullstack.Model.Persistent;

public class FrisbeeDayDbConfiguration : IEntityTypeConfiguration<FrisbeeDay>
{
	public void Configure(EntityTypeBuilder<FrisbeeDay> builder)
	{
		builder.Property(e => e.Id).ValueGeneratedOnAdd();
		builder.Property(e => e.Date).IsRequired();
		builder.Property(e => e.Conditions)
				.HasColumnType("jsonb")
				.IsRequired();

		builder.HasIndex(e => e.Date).IsUnique();
	}
}
