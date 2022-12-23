using Microsoft.Extensions.DependencyInjection;
using Morris.DomainDrivenDesign.MongoDb;

namespace Test;

internal class Program
{
	static void Main(string[] args)
	{
		var services = new ServiceCollection();
		services.UseDddMongoDb<ApplicationDbContext, Guid>(
			connectionString: "mongodb://localhost:27017",
			databaseName: "MorrisDomainDrivenDesignTest");

		IServiceProvider sp = services.BuildServiceProvider();
		var context = sp.GetRequiredService<ApplicationDbContext>();
	}
}