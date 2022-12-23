using System;

namespace Morris.DomainDrivenDesign.MongoDb;

public interface IAggregateRoot<TKey>
	where TKey : IEquatable<TKey>
{
	TKey Id { get; }
	ulong ConcurrencyVersion { get; set; }
}
