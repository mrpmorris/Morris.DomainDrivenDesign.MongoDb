using Morris.DomainDrivenDesign.MongoDb;
using Morris.DomainDrivenDesign.MongoDbTests.TestDomain;

namespace Morris.DomainDrivenDesign.MongoDbTests.EntityEntryTests;

public class EqualsTests
{
	[Fact]
	public void WhenComparedToNull_ThenReturnsFalse()
	{
		var entity = new SimpleEntity();
		var entry = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		Assert.False(entry.Equals(null));
	}

	[Fact]
	public void WhenComparedToADifferentType_ThenReturnsFalse()
	{
		var entity = new SimpleEntity();
		var entry = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		Assert.False(entry.Equals("Eggs"));
	}

	[Fact]
	public void WhenIdentical_ThenReturnsTrue()
	{
		var entity = new SimpleEntity();
		var entry1 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		var entry2 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		AssertAreEqual(entry1, entry2);
	}

	[Fact]
	public void WhenReferencingDifferentEntityInstanceWithSameId_ThenReturnsTrue()
	{
		var entity1 = new SimpleEntity();
		var entry1 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity1,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		var entity2 = new SimpleEntity { Id = entity1.Id };
		var entry2 = new EntityEntry<Guid>(
			collectionName: "X",
			entity: entity2,
			state: EntityState.Created,
			originalEntityConcurrencyVersion: 1);

		AssertAreEqual(entry1, entry2);
	}

	[Fact]
	public void WhenEntityIdIsDifferent_ThenReturnsFalse()
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

		AssertAreNotEqual(entry1, entry2);
	}

	[Fact]
	public void WhenCollectionNameIsDifferent_ThenReturnsFalse()
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

		AssertAreNotEqual(entry1, entry2);
	}

	[Fact]
	public void WhenComparing_ThenOnlyComparesEntityIdAndCollectionName()
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

		AssertAreEqual(entry1, entry2);
	}

	private static void AssertAreEqual(EntityEntry<Guid> entry1, EntityEntry<Guid> entry2)
	{
		Assert.True(entry1.Equals(entry2), ".Equals override");
		Assert.True(entry1 == entry2, "== override");
		Assert.False(entry1 != entry2, "!= override");
	}

	private static void AssertAreNotEqual(EntityEntry<Guid> entry1, EntityEntry<Guid> entry2)
	{
		Assert.False(entry1.Equals(entry2), ".Equals override");
		Assert.False(entry1 == entry2, "== override");
		Assert.True(entry1 != entry2, "!= override");
	}
}
