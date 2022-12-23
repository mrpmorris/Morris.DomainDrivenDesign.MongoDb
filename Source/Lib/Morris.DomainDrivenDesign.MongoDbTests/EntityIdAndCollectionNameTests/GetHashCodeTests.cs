using Morris.DomainDrivenDesign.MongoDb;

namespace Morris.DomainDrivenDesign.MongoDbTests.EntityIdAndCollectionNameTests;

public class GetHashCodeTests
{
	[Fact]
	public void WhenEntityIdIsDifferent_ThenReturnsDifferentHashCode()
	{
		var id1 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		var id2 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");

		Assert.NotEqual(id1.GetHashCode(), id2.GetHashCode());
	}

	[Fact]
	public void WhenCollectionNameIsDifferent_ThenReturnsDifferenceHashCode()
	{
		var id1 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		var id2 = new EntityIdAndCollectionName<Guid>(id1.EntityId, "x");

		Assert.NotEqual(id1.GetHashCode(), id2.GetHashCode());
	}

	[Fact]
	public void WhenIdentical_ThenReturnsSameHashCode()
	{
		var id1 = new EntityIdAndCollectionName<Guid>(Guid.NewGuid(), "X");
		var id2 = new EntityIdAndCollectionName<Guid>(id1.EntityId, "X");

		Assert.Equal(id1.GetHashCode(), id2.GetHashCode());
	}
}
