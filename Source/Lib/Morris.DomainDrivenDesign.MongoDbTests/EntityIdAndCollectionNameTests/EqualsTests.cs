using Morris.DomainDrivenDesign.MongoDb;

namespace Morris.DomainDrivenDesign.MongoDbTests.EntityIdAndCollectionNameTests;

public class EqualsTests
{
	[Fact]
	public void WhenComparedToNull_ThenReturnsFalse()
	{
		var subject = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		Assert.False(subject.Equals(null));
	}

	[Fact]
	public void WhenComparedToADifferentType_ThenReturnsFalse()
	{
		var subject = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		Assert.False(subject.Equals("eggs"));
	}

	[Fact]
	public void WhenIdentical_ThenReturnsTrue()
	{
		var id1 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		var id2 = new EntityIdAndCollectionName<Guid>(id1.EntityId, "X");

		AssertAreEqual(id1, id2);
	}

	[Fact]
	public void WhenEntityIdIsDifferent_ThenReturnsFalse()
	{
		var id1 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		var id2 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		
		AssertAreNotEqual(id1, id2);
	}

	[Fact]
	public void WhenCollectionNameIsDifferent_ThenReturnsFalse()
	{
		var id1 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		var id2 = new EntityIdAndCollectionName<Guid>(id1.EntityId, "x");

		AssertAreNotEqual(id1, id2);
	}

	private static void AssertAreEqual(EntityIdAndCollectionName<Guid> id1, EntityIdAndCollectionName<Guid> id2)
	{
		Assert.True(id1.Equals(id2), ".Equals override");
		Assert.True(id1 == id2, "== override");
		Assert.False(id1 != id2, "!= override");
	}

	private static void AssertAreNotEqual(EntityIdAndCollectionName<Guid> id1, EntityIdAndCollectionName<Guid> id2)
	{
		Assert.False(id1.Equals(id2), ".Equals override");
		Assert.False(id1 == id2, "== override");
		Assert.True(id1 != id2, "!= override");
	}
}
