using MongoDB.Driver;
using System;

namespace Morris.DomainDrivenDesign.MongoDb;

public abstract class DbContext<TDbContext, TKey>
	where TDbContext : DbContext<TDbContext, TKey>
	where TKey : IEquatable<TKey>
{
	private readonly IMongoClient MongoClient;
	private readonly IMongoDatabase MongoDatabase;
	private readonly DbContextOptions<TDbContext, TKey> Options;

	public DbContext(DbContextOptions<TDbContext, TKey> options)
	{
		ArgumentNullException.ThrowIfNull(options);
		Options = options;
		MongoClient = CreateMongoClient();
		MongoDatabase = CreateMongoDatabase();
	}

	protected virtual void ConfigureMongoClientSettings(MongoClientSettings mongoClientSettings)
	{
	}

	protected virtual void ConfigureMongoDatabaseSettings(MongoDatabaseSettings mongoDatabaseSettings)
	{
	}


	private MongoClient CreateMongoClient()
	{
		MongoClientSettings mongoClientSettings =
			MongoClientSettings.FromConnectionString(Options.ConnectionString);
		ConfigureMongoClientSettings(mongoClientSettings);
		return new MongoClient(mongoClientSettings);
	}

	private IMongoDatabase CreateMongoDatabase()
	{
		MongoDatabaseSettings mongoDatabaseSettings = new MongoDatabaseSettings();
		ConfigureMongoDatabaseSettings(mongoDatabaseSettings);
		return MongoClient.GetDatabase(name: Options.DatabaseName, mongoDatabaseSettings);
	}

}
