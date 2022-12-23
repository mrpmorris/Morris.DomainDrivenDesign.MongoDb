﻿using Morris.DomainDrivenDesign.MongoDb;
using Morris.DomainDrivenDesign.MongoDbTests.TestDomain;

namespace Morris.DomainDrivenDesign.MongoDbTests.EntityIdAndCollectionNameTests;

public class GetHashCodeTests
{
	[Fact]
	public void WhenEntityIdIsDifferent_ThenReturnsDifferentHashCode()
	{
		var entity1 = new SimpleEntity();
		var entry1 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity1,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		var entity2 = new SimpleEntity();
		var entry2 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity2,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		Assert.NotEqual(entry1.GetHashCode(), entry2.GetHashCode());
	}

	[Fact]
	public void WhenCollectionNameIsDifferent_ThenReturnsDifferenceHashCode()
	{
		var entity = new SimpleEntity();
		var entry1 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		var entry2 = new EntityEntry<Guid>(
			collectionName: "x",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		Assert.NotEqual(entry1.GetHashCode(), entry2.GetHashCode());
	}

	[Fact]
	public void WhenExecuted_ThenOnlyHashesEntityIdAndCollectionName()
	{
		var entity1 = new SimpleEntity();
		var entry1 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity1,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		var entity2 = new SimpleEntity() { Id = entity1.Id };
		var entry2 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity2,
			state: EntityState.Deleted,
			originalEntityConcurrencyVersion: 2);

		Assert.Equal(entry1.GetHashCode(), entry2.GetHashCode());
	}
}
