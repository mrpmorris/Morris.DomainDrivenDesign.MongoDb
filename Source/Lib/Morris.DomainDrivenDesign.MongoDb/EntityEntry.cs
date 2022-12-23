using System;

namespace Morris.DomainDrivenDesign.MongoDb;

public readonly struct EntityEntry<TKey> : IEquatable<EntityEntry<TKey>>
	where TKey: IEquatable<TKey>
{
	public EntityIdAndCollectionName<TKey> Id { get; }
	public IAggregateRoot<TKey> Entity { get; }
	public EntityState State { get; }
	public int OriginalEntityConcurrencyVersion { get; }

	internal EntityEntry(
		string collectionName,
		IAggregateRoot<TKey> entity,
		EntityState state,
		int originalEntityConcurrencyVersion)
	{
		Id = new EntityIdAndCollectionName<TKey>(entity.Id, collectionName);
		Entity = entity;
		State = state;
		OriginalEntityConcurrencyVersion = originalEntityConcurrencyVersion;
	}

	public static bool operator ==(EntityEntry<TKey> a, EntityEntry<TKey> b) => a.Equals(b);
	public static bool operator !=(EntityEntry<TKey> a, EntityEntry<TKey> b) => !a.Equals(b);

	public override int GetHashCode() => Id.GetHashCode();
	public bool Equals(EntityEntry<TKey> other) => Id.Equals(other.Id);
	public override string ToString() =>
		$"{Entity.GetType().FullName}:{Entity.Id}={State}";

	public override bool Equals(object? obj)
	{
		if (obj is not EntityEntry<TKey> other)
			return false;
		return Equals(other);
	}
}
