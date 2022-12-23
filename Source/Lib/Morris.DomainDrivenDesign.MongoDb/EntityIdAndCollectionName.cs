using System;

namespace Morris.DomainDrivenDesign.MongoDb;

public readonly struct EntityIdAndCollectionName<TKey>
	where TKey : IEquatable<TKey>
{
	public TKey EntityId { get; }
	public string CollectionName { get; }

	private readonly int CachedHashCode;

	public EntityIdAndCollectionName(TKey entityId, string collectionName)
	{
		CollectionName = collectionName;
		EntityId = entityId;
		CachedHashCode = HashCode.Combine(entityId, collectionName);
	}

	public static bool operator ==(EntityIdAndCollectionName<TKey> a, EntityIdAndCollectionName<TKey> b) => a.Equals(b);
	public static bool operator !=(EntityIdAndCollectionName<TKey> a, EntityIdAndCollectionName<TKey> b) => !a.Equals(b);

	public override int GetHashCode() => CachedHashCode;

	public override string ToString() =>
		$"{CollectionName}:{EntityId}";

	public override bool Equals(object? obj)
	{
		if (obj is not EntityIdAndCollectionName<TKey> other)
			return false;
		return Equals(other);
	}

	public bool Equals(EntityIdAndCollectionName<TKey> other) =>
		other.EntityId.Equals(EntityId)
		&& other.CollectionName == CollectionName;
}
