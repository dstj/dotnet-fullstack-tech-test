using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Fullstack.Model.Persistent;

public class MyDbContext : DbContext
{
	public DbSet<FrisbeeDay> FrisbeeDays { get; set; }

	public static DbDataSource BuildDataSource(string connectionString)
	{
		var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
		return dataSourceBuilder.Build();
	}

	public MyDbContext()
	{
	}

	public MyDbContext(DbContextOptions options)
		: base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new FrisbeeDayDbConfiguration());
	}
}
