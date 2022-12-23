using Morris.DomainDrivenDesign.MongoDb;

namespace Morris.DomainDrivenDesign.MongoDbTests.TestDomain;

internal class SimpleEntity : IAggregateRoot<Guid>
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public ulong ConcurrencyVersion { get; set; }
}
