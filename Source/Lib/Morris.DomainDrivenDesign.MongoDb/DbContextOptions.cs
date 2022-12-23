using System;

namespace Morris.DomainDrivenDesign.MongoDb;

public interface IDbContextOptions<out TDbContext, TKey>
	where TDbContext : DbContext<TDbContext, TKey>
	where TKey : IEquatable<TKey>
{
	string ConnectionString { get; }
	string DatabaseName { get; }
}

public class DbContextOptions<TDbContext, TKey> : IDbContextOptions<TDbContext, TKey>
	where TDbContext : DbContext<TDbContext, TKey>
	where TKey : IEquatable<TKey>
{
	public string ConnectionString { get; private set; }
	public string DatabaseName { get; private set; }

	public DbContextOptions(string connectionString, string databaseName)
	{
		if (string.IsNullOrWhiteSpace(connectionString))
			throw new ArgumentNullException(nameof(connectionString));
		if (string.IsNullOrWhiteSpace(databaseName))
			throw new ArgumentNullException(nameof(databaseName));

		ConnectionString = connectionString;
		DatabaseName = databaseName;
	}
}
