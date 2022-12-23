using Morris.DomainDrivenDesign.MongoDb;

namespace Test;

internal class ApplicationDbContext : DbContext<ApplicationDbContext, Guid>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext, Guid> options)
		: base(options)
	{
	}
}
