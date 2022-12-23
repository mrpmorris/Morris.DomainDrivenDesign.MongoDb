using Microsoft.Extensions.DependencyInjection;
using System;

namespace Morris.DomainDrivenDesign.MongoDb;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection UseDddMongoDb<TDbContext, TKey>(
			this IServiceCollection services,
			string connectionString,
			string databaseName,
			Func<DbContextOptions<TDbContext, TKey>, DbContextOptions<TDbContext, TKey>>? configure = null)
		where TKey : IEquatable<TKey>
		where TDbContext : DbContext<TDbContext, TKey>
	{
		ArgumentNullException.ThrowIfNull(services);
		if (string.IsNullOrWhiteSpace(connectionString))
			throw new ArgumentNullException(nameof(connectionString));
		if (string.IsNullOrWhiteSpace(databaseName))
			throw new ArgumentNullException(nameof(databaseName));

		RegisterOptions(
			services: services,
			connectionString: connectionString,
			databaseName: databaseName,
			configure: configure);
		services.AddScoped<TDbContext>();
		return services;
	}

	private static void RegisterOptions<TDbContext, TKey>(
			IServiceCollection services,
			string connectionString,
			string databaseName,
			Func<DbContextOptions<TDbContext, TKey>, DbContextOptions<TDbContext, TKey>>? configure)
		where TDbContext : DbContext<TDbContext, TKey>
		where TKey : IEquatable<TKey>
	{
		var options =
			new DbContextOptions<TDbContext, TKey>(
				connectionString: connectionString,
				databaseName: databaseName);

		if (configure is not null)
		{
			options = configure(options);
			if (options is null)
				throw new NullReferenceException($"{nameof(configure)} returned null value");
		}

		services.AddSingleton(options);
		services.AddScoped<TDbContext>();
	}
}
